import { Categoria } from './../../../interfaces/categoria';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusContaEnum } from 'src/app/enums/statusContaEnum';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { ContaMapper } from 'src/app/mappers/financas/ContaMapper';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { ValidacaoService } from 'src/app/services/validacao.service';
import { FeedbackService } from './../../../services/feedback.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-editar-conta',
  templateUrl: './editar-conta.component.html',
  styleUrls: ['./editar-conta.component.css']
})
export class EditarContaComponent implements OnInit {

  Conta: ReadConta = {
    Id: 0,
    Descricao: '',
    ValorInteiro: 0,
    ValorCentavos: 0,
    DiaVencimento: 0,
    Categoria: {} as Categoria,
    Pagamentos: [],
    UltimoPagamento: undefined,
    Variavel: false,
    StatusId: StatusContaEnum.Pendente,
  };

  TipoContaId: number = 0;
  Categorias: RespostaApi<Categoria[]>= {} as RespostaApi<Categoria[]>;

  constructor(
    private contaService: ContaService,
    private contaVariavelService: ContaVariavelService,
    private categoriaService: CategoriaService,
    private validacaoService: ValidacaoService,
    private respostaApiService: RespostaApiService,
    private feedbackService: FeedbackService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    const tipo = this.route.snapshot.paramMap.get('tipo');

    this.categoriaService.listar().subscribe({
      next: (result) => {
        this.Categorias = result;
      },
      error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
        this.router.navigate(['/contas']);
      }
    });

    if (id != null) {
      if (tipo == TipoContaEnum.Variavel.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Variavel;
        this.contaVariavelService.buscarPorId(id!).subscribe({
          next: (contaDto) => {
          this.Conta = ContaMapper.ContaVariavelDtoToConta(contaDto.valor);
          },
          error: (err) => {
            this.respostaApiService.tratarRespostaApi(err);
            this.router.navigate(['/contas']);
          }
        });
      }
      else if (tipo == TipoContaEnum.Fixa.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Fixa;
        this.contaService.buscarPorId(id!).subscribe({
          next: (contaDto) => {
          this.Conta = ContaMapper.ContaDtoToConta(contaDto.valor);
          },
          error: (err) => {
            this.respostaApiService.tratarRespostaApi(err);
            this.router.navigate(['/contas']);
          }
        });
      }
      if (this.TipoContaId == TipoContaEnum.Variavel) {
        document.getElementById("divValor")!.style.display = "none";
      }
    } else {
      this.router.navigate(['/contas']);
    }
  }

  selecionaTipoConta() {
    let tipoConta = (<HTMLInputElement>document.getElementById("tipoConta")).value;
    if (tipoConta == TipoContaEnum.Fixa.toLocaleString()) {
      document.getElementById("divValor")!.style.display = "flex";
    } else {
      document.getElementById("divValor")!.style.display = "none";
    }
  }

  salvarConta() {
    if (this.TipoContaId == TipoContaEnum.Fixa) {
      this.contaService.atualizar(this.Conta.Id.toLocaleString(), ContaMapper.ContaToUpdateContaDto(this.Conta)).subscribe({
        next: (result) => {
          this.respostaApiService.tratarRespostaApi(result);
          this.router.navigate(['/contas']);
        },
        error: (err) => {
          this.respostaApiService.tratarRespostaApi(err);
        }
      });
    }
    else if (this.TipoContaId == TipoContaEnum.Variavel){
      this.contaVariavelService.atualizar(this.Conta.Id.toLocaleString(), ContaMapper.ContaToUpdateContaVariavelDto(this.Conta)).subscribe({
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
