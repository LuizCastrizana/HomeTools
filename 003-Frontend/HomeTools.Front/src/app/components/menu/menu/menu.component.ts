import { Component, OnInit } from '@angular/core';
import { ItemMenu } from 'src/app/interfaces/item-menu';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  listaItens: ItemMenu[] = [
    {
      nome: 'Finanças',
      link: 'financas',
      icone: 'cifrao-icon.png'
    },
    {
      nome: 'Despensa',
      link: 'despensa',
      icone: 'despensa-icon.png'
    },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
