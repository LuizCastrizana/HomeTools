import { Categoria } from './../../../interfaces/categoria';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StatusContaEnum } from 'src/app/enums/statusContaEnum';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { RespostaApi } from 'src/app/interfaces/api-dto/respostaApi';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { ContaMapper } from 'src/app/mappers/financas/ContaMapper';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { ValidacaoService } from 'src/app/services/validacao.service';

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
  Categorias$: Observable<RespostaApi<Categoria[]>> = {} as Observable<RespostaApi<Categoria[]>>;

  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private serviceCategoria: CategoriaService,
    private serviceValidacao: ValidacaoService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    const tipo = this.route.snapshot.paramMap.get('tipo');
    this.Categorias$ = this.serviceCategoria.listar()
    if (id != null) {
      if (tipo == TipoContaEnum.Variavel.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Variavel;
        this.serviceContaVariavel.buscarPorId(id!).subscribe(contaDto => {
          this.Conta = ContaMapper.ContaVariavelDtoToConta(contaDto.Valor);
        });
      }
      else if (tipo == TipoContaEnum.Fixa.toLocaleString()) {
        this.TipoContaId = TipoContaEnum.Fixa;
        this.serviceConta.buscarPorId(id!).subscribe(respostaAPI => {
          this.Conta = ContaMapper.ContaDtoToConta(respostaAPI.Valor);
        });
      }
    }
    if (this.TipoContaId == TipoContaEnum.Variavel) {
      document.getElementById("divValor")!.style.display = "none";
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
      this.serviceConta.atualizar(this.Conta.Id.toLocaleString(), ContaMapper.ContaToUpdateContaDto(this.Conta)).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
    else if (this.TipoContaId == TipoContaEnum.Variavel){
      this.serviceContaVariavel.atualizar(this.Conta.Id.toLocaleString(), ContaMapper.ContaToUpdateContaVariavelDto(this.Conta)).subscribe(() => {
        this.router.navigate(['/contas']);
      });
    }
  }
  cancelar() {
    this.router.navigate(['/contas']);
  }

  apenasNumeros(event: any) {
    if (!this.serviceValidacao.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  diaMes(event: any) {
    if (!this.serviceValidacao.diaMes(event)) {
      event.preventDefault();
    }
  }
}
