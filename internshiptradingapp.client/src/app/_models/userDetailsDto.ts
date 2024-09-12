export interface Transaction {
    id: number;
    amount: number;
    date: string;
    type: string;
    bank: string;
  }
  
  export interface UserDetailsDto {
    username: string;
    email: string;
    balance: string;
    transactions: Transaction[];
  }
  