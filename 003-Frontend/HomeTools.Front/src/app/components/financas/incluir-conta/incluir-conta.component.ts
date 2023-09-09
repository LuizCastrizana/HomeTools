import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { CreateContaDto } from 'src/app/interfaces/api-dto/financas/createContaDto';
import { CreateContaVariavelDto } from 'src/app/interfaces/api-dto/financas/createContaVariavelDto';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { ValidacaoService } from 'src/app/services/validacao.service';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';
import { Categoria } from 'src/app/interfaces/categoria';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-incluir-conta',
  templateUrl: './incluir-conta.component.html',
  styleUrls: ['./incluir-conta.component.css'],
})
export class IncluirContaComponent implements OnInit {
  Conta: CreateContaDto = {} as CreateContaDto;

  TipoContaId: number = 0;
  Categorias: RespostaApi<Categoria[]> = {} as RespostaApi<Categoria[]>;

  constructor(
    private contaService: ContaService,
    private contaVariavelService: ContaVariavelService,
    private categoriaService: CategoriaService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.Conta.CategoriaId = 0;
    this.categoriaService.listar().subscribe({
      next: (result) => {
        this.Categorias = result;
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/contas']);
      }
    });
  }

  selecionaTipoConta() {
    let tipoConta = (<HTMLInputElement>document.getElementById('selTipoConta'))
      .value;
    if (tipoConta == TipoContaEnum.Fixa.toLocaleString()) {
      document.getElementById('divValor')!.style.display = 'flex';
    } else {
      document.getElementById('divValor')!.style.display = 'none';
    }
  }

  salvarConta() {
    if (this.TipoContaId == TipoContaEnum.Fixa) {
      this.contaService.incluir(this.Conta).subscribe({
        next: (result) => {
          this.respostaApiService.tratarRespostaApi(result);
          this.router.navigate(['/contas']);
        },
        error: (err) => {
          this.respostaApiService.tratarRespostaApi(err);
        }
      });
    } else if (this.TipoContaId == TipoContaEnum.Variavel) {
      let contaVariavel: CreateContaVariavelDto = {
        Descricao: this.Conta.Descricao,
        DiaVencimento: this.Conta.DiaVencimento,
        CategoriaId: this.Conta.CategoriaId,
      };
      this.contaVariavelService.incluir(contaVariavel).subscribe({
        next: (result) => {
          this.respostaApiService.tratarRespostaApi(result);
          this.router.navigate(['/contas']);
        },
        error: (err) => {
          this.respostaApiService.tratarRespostaApi(err);
        }
      });
    }
  }

  cancelar() {
    this.router.navigate(['/contas']);
  }

  apenasNumeros(event: any) {
    if (!this.validacaoService.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  diaMes(event: any) {
    if (!this.validacaoService.diaMes(event)) {
      event.preventDefault();
    }
  }
}
