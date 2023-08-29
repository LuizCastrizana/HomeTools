import { Component, OnInit } from '@angular/core';
import { ItemMenu } from 'src/app/interfaces/item-menu';

@Component({
  selector: 'app-menu-financas',
  templateUrl: './menu-financas.component.html',
  styleUrls: ['./menu-financas.component.css']
})
export class MenuFinancasComponent implements OnInit {

  listaItens: ItemMenu[] = [
    {
      nome: 'Contas',
      link: 'contas',
      icone: 'conta-icon.png'
    },
    {
      nome: 'Despesas',
      link: 'Despesas',
      icone: 'despesa-icon.png'
    },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
