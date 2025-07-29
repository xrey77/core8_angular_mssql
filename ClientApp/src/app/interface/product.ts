export interface Product {
    id: number;
    category: string;
    descriptions: string;
    qty: number;
    unit: string;
    costPrice: number;
    sellPrice: number;
    salePrice: number;
    productPicture: string;
    alertStocks: number;
    criticalStocks: number;
    createdAt: Date;
    updatedAt: Date;
}
