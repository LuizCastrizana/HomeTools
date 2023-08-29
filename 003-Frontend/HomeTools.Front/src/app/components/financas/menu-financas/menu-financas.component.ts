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
      nome: 'Finanças 01',
      link: 'financas',
      icone: 'cifrao-icon.png'
    },
    {
      nome: 'Finanças 02',
      link: 'despensa',
      icone: 'despensa-icon.png'
    },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
