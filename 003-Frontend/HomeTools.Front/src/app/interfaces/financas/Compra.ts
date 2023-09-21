import { Categoria } from "../categoria";

export interface Compra {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataCompra: Date;
  QtdParcelas: number;
  CartaoId: number;
  Categoria: Categoria;
}
