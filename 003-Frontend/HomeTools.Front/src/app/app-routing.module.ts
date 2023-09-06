import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MenuFinancasComponent } from './components/financas/menu-financas/menu-financas.component';
import { PainelContasComponent } from './components/financas/painel-contas/painel-contas.component';
import { PainelDespesasComponent } from './components/financas/painel-despesas/painel-despesas.component';
import { IncluirContaComponent } from './components/financas/incluir-conta/incluir-conta.component';
import { EditarContaComponent } from './components/financas/editar-conta/editar-conta.component';

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
  //#endregion
  //#region Despensa

  //#endregion
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
