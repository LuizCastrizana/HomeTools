import { Component, OnInit } from '@angular/core';
import { ReadConta } from 'src/app/interfaces/financas/read-conta';

@Component({
  selector: 'app-painel-contas',
  templateUrl: './painel-contas.component.html',
  styleUrls: ['./painel-contas.component.css']
})
export class PainelContasComponent implements OnInit {

  Contas: ReadConta[] = [
    {
      Id: 1,
      Descricao: 'Conta de Luz',
      ValorInteiro: 100,
      ValorCentavos: 0,
      DiaVencimento: 10,
      Categoria: {
        Id: 1,
        Descricao: 'Casa'
      }
    },
    {
      Id: 2,
      Descricao: 'Conta de Água',
      ValorInteiro: 50,
      ValorCentavos: 0,
      DiaVencimento: 15,
      Categoria: {
        Id: 1,
        Descricao: 'Casa'
      }
    }
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
