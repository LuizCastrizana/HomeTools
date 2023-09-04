import { Component, OnInit } from '@angular/core';
import { ItemMenu } from 'src/app/interfaces/itemMenu';

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
      link: 'despesas',
      icone: 'despesa-icon.png'
    },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
