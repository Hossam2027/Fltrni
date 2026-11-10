import { TestBed } from '@angular/core/testing';

import { ProviderservicesService } from './providerservices.service';

describe('ProviderservicesService', () => {
  let service: ProviderservicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProviderservicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
