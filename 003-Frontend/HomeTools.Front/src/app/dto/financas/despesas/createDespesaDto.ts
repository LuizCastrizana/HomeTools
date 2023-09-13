export interface CreateDespesaDto {
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataDespesa: number;
  QtdParcelas: number;
  CategoriaId: number;
}
