import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceproviderComponent } from './serviceprovider.component';

describe('ServiceproviderComponent', () => {
  let component: ServiceproviderComponent;
  let fixture: ComponentFixture<ServiceproviderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ServiceproviderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ServiceproviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
