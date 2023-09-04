import { Categoria } from "../../categoria";
import { PagamentoConta } from "./readPagamentoConta";

export interface ReadContaDto {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoConta[];
}
