import { TestBed } from '@angular/core/testing';

import { ServiceproviderserviceService } from './serviceproviderservice.service';

describe('ServiceproviderserviceService', () => {
  let service: ServiceproviderserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceproviderserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
