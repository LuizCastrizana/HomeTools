import { Categoria } from "../categoria";

export interface Despesa {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataDespesa: Date;
  QtdParcelas: number;
  Categoria: Categoria;
}
