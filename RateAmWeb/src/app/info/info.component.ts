import { Component } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrl: './info.component.css'
})
export class InfoComponent {
  banksList: BankInfo[] = [
    { name: "Fast Bank", link: "https://www.spyur.am/en/companies/fast-bank/51417/" },
    { name: "Unibank", link: "https://www.spyur.am/en/companies/unibank/3384/" },
    { name: "Acba Bank", link: "https://www.spyur.am/en/companies/acba-bank/1951/" },
    { name: "Inecobank", link: "https://www.spyur.am/en/companies/inecobank/310/" },
    { name: "Artsakhbank", link: "https://www.spyur.am/en/companies/artsakhbank/2459/" },
    { name: "VTB Bank (ARMENIA)", link: "https://www.spyur.am/en/companies/vtb-bank-armenia/1183/" },
    { name: "IDBank", link: "https://www.spyur.am/en/companies/id-bank/226/" },
    { name: "Byblos Bank", link: "https://www.spyur.am/en/companies/byblos-bank-armenia/3197/" },
    { name: "Mellat Bank", link: "https://www.spyur.am/en/companies/mellat-bank/1039/" },
    { name: "ArmSwissBank", link: "https://www.spyur.am/en/companies/armswissbank/5111/" },
    { name: "Ardshinbank", link: "https://www.spyur.am/en/companies/ardshinbank/3950/" },
    { name: "AraratBank", link: "https://www.spyur.am/en/companies/araratbank/410/" },
    { name: "HSBC Bank Armenia", link: "https://www.spyur.am/en/companies/hsbc-bank-armenia/2503/" },
    { name: "AMIO BANK", link: "https://www.spyur.am/en/companies/amio-bank/783/" },
    { name: "Converse Bank", link: "https://www.spyur.am/en/companies/converse-bank/1302/" },
    { name: "AMERIABANK", link: "https://www.spyur.am/en/companies/ameriabank/557/" },
    { name: "ARMECONOMBANK", link: "https://www.spyur.am/en/companies/armeconombank/974/" },
    { name: "Evocabank", link: "https://www.spyur.am/en/companies/evocabank/113/" },
  ]
}

interface BankInfo {
  name: string
  link:string
}
