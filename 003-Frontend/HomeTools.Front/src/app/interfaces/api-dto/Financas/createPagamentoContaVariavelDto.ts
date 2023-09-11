export interface CreatePagamentoContaVariavelDto {
  ContaVariavelId: number;
  ValorInteiro: number;
  ValorCentavos: number;
  DataPagamento: Date;
  MesReferencia: number;
  AnoReferencia: number;
}
