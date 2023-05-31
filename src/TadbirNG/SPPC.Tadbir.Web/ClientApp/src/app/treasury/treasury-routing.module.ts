import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CheckBookEditorComponent } from './components/check-book/check-book-editor.component';
import { CheckBookReportComponent } from './components/checkBook-report/checkBook-report.component';
import { SourcesApplicationsComponent } from './components/sources-applications/sources-applications.component';
import { PayReceiveEditorComponent } from './components/pay-receive/pay-receive-editor.component';

const routes: Routes = [
  {path:'cash-register', component:CashRegistersComponent},
  {path:'source-apps', component:SourcesApplicationsComponent},
  {path:'check-books', component:CheckBookEditorComponent},
  {path:'check-books/:mode', component:CheckBookEditorComponent},
  {path:'check-book-report', component:CheckBookReportComponent},
  {path:'receipts/:mode', component:PayReceiveEditorComponent},
  {path:'payments/:mode', component:PayReceiveEditorComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TreasuryRoutingModule { }
