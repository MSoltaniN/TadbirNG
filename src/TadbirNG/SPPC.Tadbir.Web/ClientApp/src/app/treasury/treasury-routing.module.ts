import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CheckBookComponent } from './components/check-book/check-book.component';

const routes: Routes = [
  {path:'cash-register',component:CashRegistersComponent},
  {path:'check-books',component:CheckBookComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TreasuryRoutingModule { }
