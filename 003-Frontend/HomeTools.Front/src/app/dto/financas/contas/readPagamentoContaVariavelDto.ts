export interface PagamentoContaVariavelDto {
  Id: number;
  ValorInteiro: number;
  ValorCentavos: number;
  DataPagamento: Date;
  ContaId:number
  MesReferencia: number;
  AnoReferencia: number;
}
