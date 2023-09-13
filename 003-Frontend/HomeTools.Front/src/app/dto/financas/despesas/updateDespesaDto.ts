export interface UpdateDespesaDto {
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataDespesa: Date;
  QtdParcelas: number;
  CategoriaId: number;
}
