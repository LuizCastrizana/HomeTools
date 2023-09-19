import { Component, OnInit } from '@angular/core';
import { ItemMenu } from 'src/app/interfaces/itemMenu';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

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
    this.listaItens.sort((a, b) => a.nome.localeCompare(b.nome));
  }

}
