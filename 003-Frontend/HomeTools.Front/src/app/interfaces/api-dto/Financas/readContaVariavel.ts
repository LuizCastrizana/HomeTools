import { Categoria } from "../../categoria";
import { PagamentoContaVariavel } from "./readPagamentoContaVariavel";

export interface ReadContaVariavelDto {
  Id: number;
  Descricao: string;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoContaVariavel[];
}
