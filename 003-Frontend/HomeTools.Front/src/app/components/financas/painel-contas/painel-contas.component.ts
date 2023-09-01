import { Component, OnInit } from '@angular/core';
import { ReadConta } from 'src/app/interfaces/financas/read-conta';

@Component({
  selector: 'app-painel-contas',
  templateUrl: './painel-contas.component.html',
  styleUrls: ['./painel-contas.component.css'],
})
export class PainelContasComponent implements OnInit {
  Ordem: string = 'asc';
  NomeCampo: string = '';
  Contas: ReadConta[] = [
    {
      Id: 1,
      Descricao: 'Conta de Luz',
      ValorInteiro: 100,
      ValorCentavos: 0,
      DiaVencimento: 10,
      Categoria: {
        Id: 1,
        Descricao: 'Casa',
      },
    },
    {
      Id: 2,
      Descricao: 'Conta de Água',
      ValorInteiro: 50,
      ValorCentavos: 0,
      DiaVencimento: 15,
      Categoria: {
        Id: 1,
        Descricao: 'Casa',
      },
    },
  ];
  constructor() {}

  ngOnInit(): void {
    this.ordenarContas();
  }

  ordenarContas(nomeCampo?: string) {
    if (this.Ordem == 'asc') {
      this.Ordem = 'desc';
    }
    else {
      this.Ordem = 'asc';
    }

    switch (nomeCampo) {
      case 'Descricao':
        this.NomeCampo = 'Descricao';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) =>
            a.Descricao.localeCompare(b.Descricao, 'pt-BR')
          );
        } else {
          this.Contas.sort((a, b) =>
            b.Descricao.localeCompare(a.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Categoria':
        this.NomeCampo = 'Categoria';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) =>
            a.Categoria.Descricao.localeCompare(b.Categoria.Descricao, 'pt-BR')
          );
        } else {
          this.Contas.sort((a, b) =>
            b.Categoria.Descricao.localeCompare(a.Categoria.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Valor':
        this.NomeCampo = 'Valor';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.ValorInteiro - b.ValorInteiro);
        } else {
          this.Contas.sort((a, b) => b.ValorInteiro - a.ValorInteiro);
        }
        break;
      case 'Vencimento':
        this.NomeCampo = 'Vencimento';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.DiaVencimento - b.DiaVencimento);
        } else {
          this.Contas.sort((a, b) => b.DiaVencimento - a.DiaVencimento);
        }
        break;
      default:
        this.NomeCampo = 'Id';
        this.Ordem = 'desc';
        this.Contas.sort((a, b) => b.Id - a.Id);
        break;
    }
    this.tratarIconeOrdenacao();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" class=\"img-ordenacao\">";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" class=\"img-ordenacao\">";
    let imgDescricao = document.getElementById('imgDescricao');
    let imgCategoria = document.getElementById('imgCategoria');
    let imgValor = document.getElementById('imgValor');
    let imgVencimento = document.getElementById('imgVencimento');

    imgDescricao!.innerHTML = '';
    imgCategoria!.innerHTML = '';
    imgValor!.innerHTML = '';
    imgVencimento!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'Descricao':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'Categoria':
        if (this.Ordem == 'asc') {
          imgCategoria!.innerHTML = imgAsc;
        } else {
          imgCategoria!.innerHTML = imgDesc;
        }
        break;
      case 'Valor':
        if (this.Ordem == 'asc') {
          imgValor!.innerHTML = imgAsc;
        }
        else {
          imgValor!.innerHTML = imgDesc;
        }
        break;
      case 'Vencimento':
        if (this.Ordem == 'asc') {
          imgVencimento!.innerHTML = imgAsc;
        }
        else {
          imgVencimento!.innerHTML = imgDesc;
        }
        break;
      default:
        break;
      }
  }
}
