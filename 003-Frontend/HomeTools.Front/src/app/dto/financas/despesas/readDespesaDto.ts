import { Categoria } from "src/app/interfaces/categoria";

export interface ReadDespesaDto {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataDespesa: Date;
  QtdParcelas: number;
  Categoria: Categoria;
}
