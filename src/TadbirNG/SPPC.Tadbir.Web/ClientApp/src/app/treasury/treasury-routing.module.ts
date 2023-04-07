import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CheckBookEditorComponent } from './components/check-book/check-book-editor.component';

const routes: Routes = [
  {path:'cash-register',component:CashRegistersComponent},
  {path:'check-books',component:CheckBookEditorComponent},
  {path:'check-books/:mode',component:CheckBookEditorComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TreasuryRoutingModule { }
