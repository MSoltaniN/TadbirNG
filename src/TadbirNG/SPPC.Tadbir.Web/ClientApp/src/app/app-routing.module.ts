import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "./core";
import { LayoutComponent } from "./shared/components/layout/layout.component";
import { NotFoundComponent } from "./shared/components/notFound/notFound.component";

const routes: Routes = [
  {
    path: "admin",
    component: LayoutComponent,
    canActivate: [AuthGuard],
    loadChildren: "./admin/admin.module#AdminModule",
  },
  {
    path: "finance",
    component: LayoutComponent,
    canActivate: [AuthGuard],
    loadChildren: "./finance/finance.module#FinanceModule",
  },
  {
    path: "organization",
    component: LayoutComponent,
    canActivate: [AuthGuard],
    loadChildren: "./organization/organization.module#OrganizationModule",
  },
  {
    path: "config",
    component: LayoutComponent,
    canActivate: [AuthGuard],
    loadChildren: "./config/config.module#ConfigModule",
  },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
