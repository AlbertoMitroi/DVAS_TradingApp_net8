using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipTradingApp.AccountManagement.Interfaces;
using InternshipTradingApp.AccountManagement.Services;
using InternshipTradingApp.OrderManagementSystem.Data;
using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using Microsoft.Extensions.Logging;

namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public class OrderMatchingEngineService(IOrderRepository orderRepository, ILogger<OrderMatchingEngineService> logger, IFundsService fundsService) : IOrderMatchingEngineService
    {
        public async Task MatchOrderAsync(Order newOrder)
        {
            if (newOrder.Status == OrderStatus.Processing || newOrder.Status == OrderStatus.Completed)
                return;

            if(newOrder.Quantity == 0 && newOrder.Status != OrderStatus.Completed)
            {
                newOrder.Status = OrderStatus.Completed;
                await orderRepository.UpdateAsync(newOrder);
                return;
            }

            newOrder.Status = OrderStatus.Processing;
            await orderRepository.UpdateAsync(newOrder);

            try
            {
                logger.LogInformation("Matching order {OrderId} with status {OrderStatus}.", newOrder.Id, newOrder.Status);

                var pendingOrders = await orderRepository.GetAllPendingAsync();
                var oppositeOrders = pendingOrders
                    .Where(o => o.StockSymbol == newOrder.StockSymbol &&
                                o.Type != newOrder.Type &&
                                o.Status == OrderStatus.Pending &&
                                IsPriceMatch(newOrder, o))
                    .OrderBy(o => o.Price)
                    .ToList();

                bool isOrderMatched = false;

                foreach (var oppositeOrder in oppositeOrders)
                {
                    if (newOrder.CustomerId == oppositeOrder.CustomerId)
                        break;
                    if(newOrder.Type == OrderType.Buy)
                    {
                    await ExecuteMatching(newOrder, oppositeOrder);
                    }
                    else
                    {
                        await ExecuteMatching(oppositeOrder, newOrder);
                    }

                    if (newOrder.Status == OrderStatus.Completed)
                    {
                        logger.LogInformation("Order {OrderId} completed matching with order {OppositeOrderId}.", newOrder.Id, oppositeOrder.Id);
                        isOrderMatched = true;
                        break;
                    }
                }

                if (!isOrderMatched)
                {
                    newOrder.Status = OrderStatus.Pending;
                    await orderRepository.UpdateAsync(newOrder);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during matching process for order {OrderId}.", newOrder.Id);

                newOrder.Status = OrderStatus.Failed; 
                await orderRepository.UpdateAsync(newOrder);
            }
        }


        public async Task MatchPendingOrdersAsync()
        {
            var pendingOrders = (await orderRepository.GetAllPendingAsync()).ToList();

            logger.LogInformation("Matching {Count} pending orders.", pendingOrders.Count);

            foreach (var order in pendingOrders)
            {
                await MatchOrderAsync(order);
            }
        }

        private bool IsPriceMatch(Order order1, Order order2)
        {
            decimal priceTolerance = 0.10m;
            return order1.Price >= order2.Price * (1 - priceTolerance) &&
                   order1.Price <= order2.Price * (1 + priceTolerance);
        }

        private async Task ExecuteMatching(Order buyOrder, Order sellOrder)
        {
            int buyOrderQuantity = (int)Math.Round(buyOrder.Quantity);
            int sellOrderQuantity = (int)Math.Round(sellOrder.Quantity);

            decimal transactionAmount = sellOrder.Price * Math.Min(buyOrderQuantity, sellOrderQuantity);

            if (buyOrderQuantity == sellOrderQuantity)
            {
                buyOrder.Status = OrderStatus.Completed;
                sellOrder.Status = OrderStatus.Completed;

                await fundsService.ReturnFundsAsync(sellOrder.CustomerId, transactionAmount);
            }
            else if (buyOrder.Quantity > sellOrder.Quantity)
            {
                buyOrder.Quantity -= sellOrder.Quantity;

                if (buyOrder.Quantity == sellOrder.Quantity)
                    buyOrder.Status = OrderStatus.Completed;

                sellOrder.Status = OrderStatus.Completed;
                await fundsService.ReturnFundsAsync(sellOrder.CustomerId, transactionAmount);
            }
            else
            {
                sellOrder.Quantity -= buyOrder.Quantity;

                if (buyOrder.Quantity == sellOrder.Quantity)
                {
                    sellOrder.Status = OrderStatus.Completed;
                    sellOrder.Quantity += buyOrder.Quantity;
                }

                buyOrder.Status = OrderStatus.Completed;
                await fundsService.ReturnFundsAsync(sellOrder.CustomerId, transactionAmount);
            }

            if (sellOrder.Quantity == 0)
            {
                sellOrder.Status = OrderStatus.Completed;
                await orderRepository.UpdateAsync(sellOrder);
            }
            if (buyOrder.Quantity == 0)
            {
                buyOrder.Status = OrderStatus.Completed;
                await orderRepository.UpdateAsync(buyOrder);
            }

            logger.LogInformation("Executing matching: BuyOrder {BuyOrderId}, SellOrder {SellOrderId}.", buyOrder.Id, sellOrder.Id);

            await orderRepository.UpdateAsync(buyOrder);
            await orderRepository.UpdateAsync(sellOrder);
        }

    }
}
