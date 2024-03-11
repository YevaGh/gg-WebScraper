import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RatesService } from '../service/rates.service';
import { Bank } from '../models/bank';
import { Currency } from '../models/currency';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Rate } from '../models/rate';
import { TableRow } from '../models/tableRow';

@Component({
  standalone: true,
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.css',
  imports: [MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule, CommonModule],
})
export class CalculatorComponent implements OnChanges {
  allBanks: Bank[] = [];
  selectedBank!: Bank;
  curs!: Map<number, Currency>;
  rates!: Rate[];

  amount: number = 1
  fromCurrency!: string
  toCurrency!: string
  convertedAmount?: number = 0
  availableCurrencies: string[] = []

  @Input() selectedRow: TableRow | undefined;
  @Input() clickedCurrency: Currency | undefined;
  constructor(private rateService: RatesService) { }

  ngOnInit(): void {
    this.rateService.getRates().subscribe(
      (res: Rate[]) => {
        this.rates = res;
      }
    )
    this.rateService.getBanksAndCurrencies().subscribe(
      (res: { banks: Map<number, Bank>, currencies: Map<number, Currency> }) => {
        this.allBanks = Array.from(res.banks.values())
        this.curs = res.currencies
        this.selectedBank = res.banks.get(this.getHighestBuyValueBank())!

        this.availableCurrencies = this.getAvailableCurrencies(this.selectedBank);
        this.fromCurrency = this.availableCurrencies[1];
        this.toCurrency = this.availableCurrencies[0];
        this.convertedAmount = this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == 1)!.buyRate
      }
    )

  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedRow'] || changes['clickedCurrency']) {
      this.selectedBank = this.allBanks.find(b => b.name == this.selectedRow!.bank)!
      this.fromCurrency = this.clickedCurrency!.name
    }
  }

  getAvailableCurrencies(bank: Bank): string[] {
    const currIds = this.rates.filter(rate => rate.bankId === bank.id && rate.buyRate != 0).map(r => r.currencyId)
    var currencies = currIds.map(key => this.curs.get(key)!.name)
    currencies.unshift("AMD")
    return currencies
  }

  convert(): void {
    if (this.fromCurrency == this.toCurrency) {
      this.convertedAmount = this.amount
      return
    }
    else if (this.fromCurrency == "AMD") {
      var float = this.amount! / this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.toCurrency))!.sellRate
      this.convertedAmount = parseFloat(float.toFixed(2))
      return
    }
    else {
      var buyRate = this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.fromCurrency))!.buyRate

      if (this.toCurrency == "AMD") {
        this.convertedAmount = buyRate * this.amount
      } else {
        var toCurrSellRate = this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.toCurrency))!.sellRate
        this.convertedAmount = parseFloat(((buyRate / toCurrSellRate) * this.amount).toFixed(2))
      }
    }

  }

  convertReverse(): void {
    if (this.fromCurrency == this.toCurrency) {
      this.convertedAmount = this.amount
      return
    } if (this.fromCurrency == "AMD") {
      var rate = this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.toCurrency))!
      this.amount = parseFloat((rate.sellRate * this.convertedAmount!).toFixed(2))
    } else {
      var rate = this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.toCurrency))!
      var toAMD = rate.sellRate * this.convertedAmount!
      this.amount = parseFloat((toAMD / this.rates.find(r => r.bankId == this.selectedBank.id && r.currencyId == this.getIdByName(this.fromCurrency))!.buyRate).toFixed(2))
    }
  }

  currenciesReverse() {
    var cur1 = this.fromCurrency
    this.fromCurrency = this.toCurrency
    this.toCurrency = cur1
    this.convert()
  }

  onBankChange(): void {
    this.availableCurrencies = this.getAvailableCurrencies(this.selectedBank);

    if (!this.availableCurrencies.includes(this.fromCurrency)) {
      this.fromCurrency = this.availableCurrencies[0];
    }
    if (!this.availableCurrencies.includes(this.toCurrency)) {
      this.toCurrency = this.availableCurrencies[1];
    }
    this.convert();
  }


  private getHighestBuyValueBank(): number {
    const filteredRates = this.rates.filter(x => x.currencyId == 1 && x.buyRate !== 0)
    const maxBuyRateBank = filteredRates.reduce((maxRate, rate) => (rate.buyRate > maxRate.buyRate ? rate : maxRate), filteredRates[0]).bankId;
    return maxBuyRateBank
  }

  private getIdByName(value: string): number {
    const id = [...this.curs.entries()].find(([_, val]) => val.name === value)![0];
    return id;
  }
}

