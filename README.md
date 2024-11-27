# DVAS Trading App 📊

**A robust and user-friendly stock trading application built with .NET 8 and Angular, showcasing best practices in modern software development. This project combines real-time data processing, secure transactions, and a scalable architecture to deliver an exceptional trading simulation experience.**

---

## **🌟 Overview**

The **DVAS Trading App** is designed to simulate real-world trading scenarios for users ranging from novice to expert traders. With features like real-time market data, secure trading functionalities, and an intuitive dashboard, the app aims to provide a comprehensive platform for managing virtual investments. 

This monolithic application leverages **.NET 8** for backend services and **Angular** for a responsive frontend, ensuring seamless user interaction and performance.

---

## **🔑 Key Features**

### **1. User Management**
- **Sign Up**: Create accounts with email, username, and password.
- **Login**: Secure authentication for returning users.
- **Password Recovery**: Easily reset forgotten passwords.
- **Role-Based Access Control**: 
  - Admin: System management.
  - User: Trading access.

### **2. Trading Dashboard**
- Portfolio Overview: Current balance, stock holdings, and recent transactions.
- Key Metrics: Total profit/loss, available balance, and investment value.
- Detailed Stock Insights: Charts, company details, and latest news.

### **3. Market Data**
- **Real-Time Stock Prices**: Fetch and display live updates for stocks.
- **Historical Data**: Interactive price charts for trend analysis.
- **Comprehensive Stock Details**: Market insights and stock-specific news.

### **4. Trading Operations**
- Place **Buy** and **Sell** orders.
- Support for **Market**, **Limit**, and **Stop Orders**.
- Track and manage transaction history.
- Portfolio Management: Analyze and adjust stock holdings.

---

## **🧩Non-Functional Requirements**

### **Performance**
- Real-time updates with minimal latency.
- Handles high concurrency and transaction volumes.

### **Scalability**
- Designed for future growth with scalable cloud infrastructure.

### **Security**
- Encrypted data transmission and secure storage.
- Robust user authentication and authorization.

### **Usability**
- Intuitive user interface with responsive design.
- Accessible on desktops, tablets, and smartphones.

### **Reliability**
- High availability with minimal downtime.
- Automated data backups and recovery systems.

### **Maintainability**
- Modular, well-documented codebase.
- Continuous Integration/Continuous Deployment (CI/CD).

### **Compliance**
- GDPR-compliant user data protection.
- Adherence to financial regulations for trading simulation.

---

## **🖥️ Technologies Used**

### **Backend**
- **.NET 8**: For server-side logic and API development.
- **Entity Framework Core**: Simplified database management with code-first migrations.
- **Azure Services**:
  - Azure SQL for persistent storage.
  - Redis Cache for performance optimization.

### **Frontend**
- **Angular**: Responsive and interactive UI.
- **Chart.js**: Real-time and historical stock price charts.

---

## **🚀 How to Run Locally**
1. Clone the repository:  
   ```bash
   git clone https://github.com/AlbertoMitroi/DVAS_TradingApp_net8.git
3. Navigate to the project directory:  
   ```bash
   cd DVAS_TradingApp_net8
5. Build and run the application:  
   - Backend:
     ```bash
     dotnet run
   - Frontend:
     ```bash
     ng serve #(from the `client` directory)

Access the application at [http://localhost:4200](http://localhost:4200).

---

## **☁️ Cloud Deployment**
- Hosted on **Azure App Service** with automated CI/CD pipelines.
- Backend and frontend deployments managed through **Azure Pipelines**.
- Database integration with **Azure SQL** and **SQLServer**.

---

## **📄 Documentation**
Detailed architectural diagrams and design principles are available in the [documentation](https://github.com/AlbertoMitroi/DVAS_TradingApp_net8/tree/main/documentation).  
Refer to the [Architecture PDF](https://github.com/AlbertoMitroi/DVAS_TradingApp_net8/blob/main/documentation/Internship_TradingApp.pdf) for insights into the system design and components.

---

## **🎉 Conclusion**
The DVAS Trading App is a testament to advanced software development techniques and scalable architectures. Explore the repository to gain insights into its implementation and capabilities.

Feel free to connect for any queries or collaboration opportunities!
