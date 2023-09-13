import { Despesa } from 'src/app/interfaces/financas/Despesa';
import { ReadDespesaDto } from './../../dto/financas/despesas/readDespesaDto';
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class DespesaMapper {

  constructor () {}

  DespesaDtoToDespesa(ReadDespesaDto: ReadDespesaDto): Despesa {
    let Despesa: Despesa = {
      Id: ReadDespesaDto.Id,
      Descricao: ReadDespesaDto.Descricao,
      ValorInteiro: ReadDespesaDto.ValorInteiro,
      ValorCentavos: ReadDespesaDto.ValorCentavos,
      DataDespesa: ReadDespesaDto.DataDespesa,
      QtdParcelas: ReadDespesaDto.QtdParcelas,
      Categoria: ReadDespesaDto.Categoria,
    }
    return Despesa;
  }

}

