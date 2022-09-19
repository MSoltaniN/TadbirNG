import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabWidgetComponent } from './tab-widget.component';

describe('TabWidgetComponent', () => {
  let component: TabWidgetComponent;
  let fixture: ComponentFixture<TabWidgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabWidgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
