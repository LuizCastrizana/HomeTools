import { Component, Input, OnInit } from '@angular/core';
import { ItemMenu } from 'src/app/interfaces/itemMenu';

@Component({
  selector: 'app-botao-menu',
  templateUrl: './botao-menu.component.html',
  styleUrls: ['./botao-menu.component.css']
})
export class BotaoMenuComponent implements OnInit {

  @Input() itemMenu: ItemMenu = {
    nome: '',
    link: '',
    icone: '',
  };

  constructor() { }

  ngOnInit(): void {
  }

}
