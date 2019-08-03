import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigRoutingModule } from './config-routing.module';

import { SettingService } from '@sppc/config';

import { SettingsComponent } from '@sppc/config/components/settings/settings.component';
import { SettingsFormComponent } from '@sppc/config/components/settings/settings-form.component';
import { ViewTreeConfigComponent } from '@sppc/config/components/viewTreeConfig/viewTreeConfig.component';

@NgModule({
  imports: [
    CommonModule,
    ConfigRoutingModule
  ],
  declarations: [SettingsComponent, SettingsFormComponent, ViewTreeConfigComponent],
  providers: [SettingService]
})
export class ConfigModule { }
