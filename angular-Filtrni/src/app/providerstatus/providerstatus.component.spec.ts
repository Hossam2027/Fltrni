import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProviderstatusComponent } from './providerstatus.component';

describe('ProviderstatusComponent', () => {
  let component: ProviderstatusComponent;
  let fixture: ComponentFixture<ProviderstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProviderstatusComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProviderstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
