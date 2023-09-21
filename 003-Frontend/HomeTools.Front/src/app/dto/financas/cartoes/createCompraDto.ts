export interface CreateCompraDto {
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataCompra: Date;
  QtdParcelas: number;
  CartaoId: number;
  CategoriaId: number;
}
