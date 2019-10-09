﻿using application.cqrs.auditTrail.queries;
using AutoMapper;
using lib.test.infrastructure;
using persistence;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace lib.test.cqrs_tests
{
    [Collection("QueryCollection")]
    public class AuditTrailsTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuditTrailsTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAuditTrails()
        {
            var sut = new GetAuditTrailsHandler(_context, _mapper);

            var result = await sut.Handle(new GetAuditTrailsQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetAuditTrailsResult>();

        }
    }
}