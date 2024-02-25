export interface TableRow {
  bankIcon:string
  bank: string
  date: Date
  currencies: CurrencyBuySell[]
}

export interface CurrencyBuySell {
  curName: string
  buy: number
  sell:number
}
