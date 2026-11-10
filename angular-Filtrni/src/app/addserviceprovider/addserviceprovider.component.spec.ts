import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddserviceproviderComponent } from './addserviceprovider.component';

describe('AddserviceproviderComponent', () => {
  let component: AddserviceproviderComponent;
  let fixture: ComponentFixture<AddserviceproviderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddserviceproviderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddserviceproviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
