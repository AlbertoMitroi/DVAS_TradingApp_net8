export interface CompanyHistoryGetDTO {
    id: number; // În TypeScript, ulong se mapă la number
    companySymbol: string;
    price: number;
    referencePrice: number;
    openingPrice: number;
    closingPrice: number;
    per: number; // PER se mapă la per
    dayVariation: number;
    eps: number; // EPS se mapă la eps
    date: string; // DateOnly în TypeScript este tratat ca un string în formatul ISO
    volume: number;
  }
  
  export interface CompanyGetDTO {
    id: number;
    name: string;
    symbol: string;
    status: number;
    history: CompanyHistoryGetDTO[];
  }
  
  export interface CompanyWithHistoryGetDTO {
    company: CompanyGetDTO;
    history: CompanyHistoryGetDTO[];
  }
  