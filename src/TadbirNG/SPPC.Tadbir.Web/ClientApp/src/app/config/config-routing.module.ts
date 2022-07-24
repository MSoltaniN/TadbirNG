import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SettingsComponent } from '@sppc/config/components/settings/settings.component';
import { AuthGuard } from '@sppc/core';
import { LayoutComponent } from '@sppc/shared/components/layout/layout.component';

const routes: Routes = [
  { path: 'config', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'settings', component: SettingsComponent },
    ]
  }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigRoutingModule { }
