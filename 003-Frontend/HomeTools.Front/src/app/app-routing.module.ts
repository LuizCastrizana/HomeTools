import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MenuFinancasComponent } from './components/financas/menu-financas/menu-financas.component';
import { PainelContasComponent } from './components/financas/contas/painel-contas/painel-contas.component';
import { PainelDespesasComponent } from './components/financas/despesas/painel-despesas/painel-despesas.component';
import { IncluirContaComponent } from './components/financas/contas/incluir-conta/incluir-conta.component';
import { EditarContaComponent } from './components/financas/contas/editar-conta/editar-conta.component';
import { IncluirDespesaComponent } from './components/financas/despesas/incluir-despesa/incluir-despesa.component';
import { EditarDespesaComponent } from './components/financas/despesas/editar-despesa/editar-despesa.component';

const routes: Routes = [
  //#region Home
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  //#endregion
  //#region Finanças
  {
    path: 'financas',
    component: MenuFinancasComponent
  },
  {
    path: 'contas',
    component: PainelContasComponent
  },
  {
    path: 'despesas',
    component: PainelDespesasComponent
  },
  {
    path: 'contas/incluir-conta',
    component: IncluirContaComponent
  },
  {
    path: 'contas/editar-conta/:id/:tipo',
    component: EditarContaComponent
  },
  {
    path: 'despesas/incluir-despesa',
    component: IncluirDespesaComponent
  },
  {
    path: 'despesas/editar-despesa/:id',
    component: EditarDespesaComponent
  },
  //#endregion
  //#region Despensa

  //#endregion
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
