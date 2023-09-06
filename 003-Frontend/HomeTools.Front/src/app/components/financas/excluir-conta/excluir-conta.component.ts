import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TipoContaEnum } from 'src/app/enums/tipoContaEnum';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { ContaMapper } from 'src/app/mappers/financas/ContaMapper';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ContaService } from 'src/app/services/Financas/conta.service';

@Component({
  selector: 'app-excluir-conta',
  templateUrl: './excluir-conta.component.html',
  styleUrls: ['./excluir-conta.component.css']
})
export class ExcluirContaComponent implements OnInit {

  Conta: ReadConta = {} as ReadConta;
  constructor(
    private serviceConta: ContaService,
    private serviceContaVariavel: ContaVariavelService,
    private router: Router,
    private route: ActivatedRoute,
    ) { }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');
    let tipo = parseInt(this.route.snapshot.paramMap.get('tipo')!);
    if (tipo == TipoContaEnum.Variavel) {
      this.serviceContaVariavel.buscarPorId(id!).subscribe(contaDto => {
        this.Conta = ContaMapper.ContaVariavelDtoToConta(contaDto.Valor);
      });
    }
    else if (tipo == TipoContaEnum.Fixa) {
      this.serviceConta.buscarPorId(id!).subscribe(contaDto => {
        this.Conta = ContaMapper.ContaDtoToConta(contaDto.Valor);
      });
    }
  }

  excluirConta() {
    if (this.Conta.Variavel) {
      this.serviceContaVariavel.excluir(this.Conta.Id.toString()).subscribe(() => {
        this.router.navigate(['contas']);
      });
    }
    else {
      this.serviceConta.excluir(this.Conta.Id.toString()).subscribe(() => {
        this.router.navigate(['contas']);
      });
    }
  }

  cancelar() {
    this.router.navigate(['contas']);
  }

}
