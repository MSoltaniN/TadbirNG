import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '@sppc/shared/components/layout/layout.component';
import { AuthGuard } from '@sppc/core';
import { SettingsComponent } from '@sppc/config/components/settings/settings.component';

const routes: Routes = [{
  path: 'config',
  component: LayoutComponent,
  canActivate: [AuthGuard],
  children: [
    { path: 'settings', component: SettingsComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigRoutingModule { }
