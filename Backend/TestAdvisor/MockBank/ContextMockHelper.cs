using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestAdvisor.Tests.MockBank
{
    public static class ContextMockHelper
    {
        public static Mock<DbSet<Domain.Models.Advisor>> GetAdvisorMockSet()
        {
            var data = new List<Domain.Models.Advisor>
            {
                new()
                {
                    AdvisorId= 35,
                    Name = "jay",
                    Sin = "748596123",
                    Address = "asdasdaas nasd asd asdasd asd asd as das",
                    Phone = "12345678",
                    Created = new DateTime(),
                    CreatedBy = 0,
                    Updated = new DateTime(),
                    UpdatedBy = 0
                },
                new()
                {
                    AdvisorId= 36,
                    Name = "Test",
                    Sin = "748596125",
                    Address = "asdasdaas nasd asd asdasd asd asd as das",
                    Phone = "12345678",
                    Created = new DateTime(),
                    CreatedBy = 0,
                    Updated = new DateTime(),
                    UpdatedBy = 0
                }
            };

            var lessonMockSet = CreateDbSetMock(data.AsQueryable());
            lessonMockSet
            .Setup(m => m.AddAsync(It.IsAny<Domain.Models.Advisor>(), default))
              .Callback<Domain.Models.Advisor, CancellationToken>((s, _) =>
              {
                  s.AdvisorId = data.Count + 1;
                  data.Add(s);
              });

            lessonMockSet.Setup(m => m.Remove(It.IsAny<Domain.Models.Advisor>()))
              .Callback<Domain.Models.Advisor>(s =>
              {
                  data.Remove(data.Find(t => t.AdvisorId == s.AdvisorId));
              });

            lessonMockSet
              .Setup(m => m.FindAsync(It.IsAny<object[]>()))
              .Returns((object[] r) =>
              {
                  var advisor = lessonMockSet.Object
            .FirstOrDefaultAsync(b => b.AdvisorId == (int)r[0]);
                  return new ValueTask<Domain.Models.Advisor>(advisor);
              });

            return lessonMockSet;
        }
        /*public static Mock<DbSet<SchedulerTask>> GetSchedulerTaskMockSet()
        {
          var data = new List<SchedulerTask>
          {
            new SchedulerTask
            {
              SchedulerTaskId = 1,
              OwnerId = 1,
              Title = "Hello My New Task",
              Description = "Some comments",
              StartTimeZone = "America/Chicago",
              Start = new DateTime(),
              End = new DateTime().AddHours(2),
              EndTimeZone = "America/Chicago",
              RecurrenceRule = null,
              IsAllDay = false,
              Created = new DateTime(),
              CreatedBy = "auth0|612e924e6ca96a0069bb514d",
              Updated = new DateTime(),
              UpdatedBy = "auth0|612e924e6ca96a0069bb514d",
              SchedulerTaskTypeId = 3,
              Completed = null,
              Deleted = null,
              SchedulerTaskStatusId = null,
              Note = null,
              NotificationSent = null,
              IsLateEntry = false,
              SchedulerTaskTypeClassId = null,
              SchedulerTaskSubTypeId = null,
              SchedulerTaskSubTypeText = "",
              SchedulerTaskOutcomeId = null,
              SchedulerTaskPurposeId = null,
              ParentSchedulerTaskId = null,
              IsLastEntry = null,
              RecurringTaskParentTaskId = null,
              LocationId = null,
              FollowUpReason = "",
              LiaisonBeingObservedId = null,
              FollowUpParentTaskId = null,
              Owner = new User()
              {
                UserId = 1,
                Created = new DateTime(),
                CreatedBy = "SYSTEM",
                Updated = new DateTime(),
                UpdatedBy = "SYSTEM",
                ExternalId  = "auth0|612e924e6ca96a0069bb514d",
                Email  = "test.optimaflow@gmail.com",
                PhoneNumber = "8887775555",
                FirstName = "Optima",
                LastName  = "Test",
                LastLogin  =  new DateTime(),
                Deleted  = null,
                DisabledOn  = null,
                Disabled  = false
              }
            }
          };

          var schedulerTaskMockSet = CreateDbSetMock(data.AsQueryable());
          schedulerTaskMockSet
            .Setup(m => m.AddAsync(It.IsAny<SchedulerTask>(), default))
            .Callback<SchedulerTask, CancellationToken>((s, _) =>
            {
              s.SchedulerTaskId = data.Count + 1;
              data.Add(s);
            });

          schedulerTaskMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var schedulerTask = schedulerTaskMockSet.Object
              .FirstOrDefaultAsync(b => b.SchedulerTaskId == (int)r[0]);
              return new ValueTask<SchedulerTask>(schedulerTask);
            });

          return schedulerTaskMockSet;
        }

        public static Mock<DbSet<ReferralSource>> GetReferralSourceMockSet()
        {
          return CreateDbSetMock(new List<ReferralSource>().AsQueryable());
        }

        public static Mock<DbSet<ReferralSourceNote>> GetReferralSourceNoteMockSet()
        {
          return CreateDbSetMock(new List<ReferralSourceNote>().AsQueryable());
        }

        public static Mock<DbSet<OrganizationNote>> GetOrganizationNoteMockSet()
        {
          return CreateDbSetMock(new List<OrganizationNote>().AsQueryable());
        }

        public static Mock<DbSet<SchedulerTaskOutcome>> GetSchedulerTaskOutcomeMockSet()
        {
          var data = new List<SchedulerTaskOutcome>
          {
            new()
            {
              SchedulerTaskOutcomeId = 1,
              Name = "Received New Referral",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskOutcomeId = 2,
              Name = "Turned Away",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskOutcomeId = 3,
              Name = "Follow up Meeting Scheduled",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            }
          };
          return CreateDbSetMock(data.AsQueryable());
        }

        public static Mock<DbSet<SchedulerTaskPurpose>> GetSchedulerTaskPurposeMockSet()
        {
          var data = new List<SchedulerTaskPurpose>
          {
            new()
            {
              SchedulerTaskPurposeId = 1,
              Name = "New Lead Generation",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 2,
              Name = "Establish Rapport (New Account)",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 3,
              Name = "Establish Rapport (New Referral Source)",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 4,
              Name = "Introduction (Cold Call)",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 5,
              Name = "Lead Nurturing",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 6,
              Name = "Follow up with Outcomes provided",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 7,
              Name = "Conflict Resolution",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 8,
              Name = "Re-evaluate Account tier status",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskPurposeId = 9,
              Name = "Other",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
          };
          return CreateDbSetMock(data.AsQueryable());
        }

        public static Mock<DbSet<SchedulerTaskStatus>> GetSchedulerTaskStatusMockSet()
        {
          var data = new List<SchedulerTaskStatus>
          {
            new()
            {
              SchedulerTaskStatusId = 1,
              Name = "Open",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 2,
              Name = "Missed",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 3,
              Name = "Completed",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 4,
              Name = "Deleted",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 5,
              Name = "Open",
              SchedulerTaskTypeId = 2,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 6,
              Name = "Missed",
              SchedulerTaskTypeId = 2,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 7,
              Name = "Completed",
              SchedulerTaskTypeId = 2,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 8,
              Name = "Deleted",
              SchedulerTaskTypeId = 2,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 9,
              Name = "Open",
              SchedulerTaskTypeId = 3,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 10,
              Name = "Overdue",
              SchedulerTaskTypeId = 3,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 11,
              Name = "Completed",
              SchedulerTaskTypeId = 3,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 12,
              Name = "Deleted",
              SchedulerTaskTypeId = 3,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 13,
              Name = "Open",
              SchedulerTaskTypeId = 4,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 14,
              Name = "Missed",
              SchedulerTaskTypeId = 4,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 15,
              Name = "Completed",
              SchedulerTaskTypeId = 4,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 16,
              Name = "Deleted",
              SchedulerTaskTypeId = 4,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 21,
              Name = "Cancelled",
              SchedulerTaskTypeId = 2,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 22,
              Name = "Cancelled",
              SchedulerTaskTypeId = 4,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 23,
              Name = "Cancelled",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 24,
              Name = "Cancelled",
              SchedulerTaskTypeId = 3,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 21,
              Name = "",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 22,
              Name = "",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 23,
              Name = "",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            },
            new()
            {
              SchedulerTaskStatusId = 24,
              Name = "",
              SchedulerTaskTypeId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null
            }
          };
          return CreateDbSetMock(data.AsQueryable());
        }

        public static Mock<DbSet<Location>> GetLocationMockSet()
        {
          var data = new List<Location>
          {
            new()
            {
              LocationId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Name = "Name",
              AddressLine1 = "Line1",
              AddressLine2 = "Line2",
              City = "Dallas",
              StateCode = "TX",
              ZipCode = "12345",
              LocationAlias = "alias",
              AdmissionsGroupEmailAddress = "test@example.com",
              LiaisonGroupEmailAddress = "1test@example.com",
              TimeZone = "America/Chicago",
              Deleted = null,
              Disabled = false,
              WinTimeZone = "Central Standard Time",
              SortOrder = null,
              EnableCensus = true,
              EnableMidDayText = false
            }
          };

          var locationMockSet = CreateDbSetMock(data.AsQueryable());
          locationMockSet
            .Setup(m => m.AddAsync(It.IsAny<Location>(), default))
            .Callback<Location, CancellationToken>((s, _) =>
            {
              s.LocationId = data.Count + 1;
              data.Add(s);
            });

          locationMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var location = locationMockSet.Object.FirstOrDefaultAsync(b => b.LocationId == (int)r[0]);
              return new ValueTask<Location>(location);
            });

          return locationMockSet;
        }

        public static Mock<DbSet<Role>> GetRoleMockSet()
        {
          var data = new List<Role>
          {
            new()
            {
              RoleId = 1,
              Name = "Admin",
              Description = "Administrator",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              ExternalId  = "rol_ClXDqs17eIxxX51X",
              Deleted  = null,
              Disabled  = false
            }
          };

          var roleMockSet = CreateDbSetMock(data.AsQueryable());
          roleMockSet
            .Setup(m => m.AddAsync(It.IsAny<Role>(), default))
            .Callback<Role, CancellationToken>((s, _) =>
            {
              s.RoleId = data.Count + 1;
              data.Add(s);
            });

          roleMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var role = roleMockSet.Object.FirstOrDefaultAsync(b => b.RoleId == (int)r[0]);
              return new ValueTask<Role>(role);
            });

          return roleMockSet;
        }

        public static Mock<DbSet<User>> GetUserMockSet()
        {
          var data = new List<User>
          {
            new()
            {
              UserId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              ExternalId  = "auth0|612e924e6ca96a0069bb514d",
              Email  = "test.optimaflow@gmail.com",
              PhoneNumber = "8887775555",
              FirstName = "Optima",
              LastName  = "Test",
              LastLogin  =  new DateTime(),
              Deleted  = null,
              DisabledOn  = null,
              Disabled  = false
            }
          };

          var userMockSet = CreateDbSetMock(data.AsQueryable());
          userMockSet
            .Setup(m => m.AddAsync(It.IsAny<User>(), default))
            .Callback<User, CancellationToken>((s, _) =>
            {
              s.UserId = data.Count + 1;
              data.Add(s);
            });

          userMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var user = userMockSet.Object.FirstOrDefaultAsync(b => b.UserId == (int)r[0]);
              return new ValueTask<User>(user);
            });

          return userMockSet;
        }

        public static Mock<DbSet<UserLocation>> GetUserLocationMockSet()
        {
          var data = new List<UserLocation>
          {
            new()
            {
              UserLocationId = 1,
              LocationId = 1,
              UserId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted = null,
              User = new User
              {
                UserId = 1,
                Created = new DateTime(),
                CreatedBy = "SYSTEM",
                Updated = new DateTime(),
                UpdatedBy = "SYSTEM",
                ExternalId  = "auth0|612e924e6ca96a0069bb514d",
                Email  = "test.optimaflow@gmail.com",
                PhoneNumber = "8887775555",
                FirstName = "Optima",
                LastName  = "Test",
                LastLogin  =  new DateTime(),
                Deleted  = null,
                DisabledOn  = null,
                Disabled  = false
              },
            }
          };

          var userLocationMockSet = CreateDbSetMock(data.AsQueryable());
          userLocationMockSet
            .Setup(m => m.AddAsync(It.IsAny<UserLocation>(), default))
            .Callback<UserLocation, CancellationToken>((s, _) =>
            {
              s.UserId = data.Count + 1;
              data.Add(s);
            });

          userLocationMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var userLocation = userLocationMockSet.Object.FirstOrDefaultAsync(b => b.UserLocationId == (int)r[0]);
              return new ValueTask<UserLocation>(userLocation);
            });

          return userLocationMockSet;
        }

        public static Mock<DbSet<SchedulerTaskInternalAttendee>> GetSchedulerTaskInternalAttendeeMockSet()
        {
          var data = new List<SchedulerTaskInternalAttendee>
          {
            new()
            {
              SchedulerTaskInternalAttendeeId = 1,
              SchedulerTaskId = 1,
              UserId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null,
            }
          };

          var schedulerTaskInternalAttendeeMockSet = CreateDbSetMock(data.AsQueryable());
          schedulerTaskInternalAttendeeMockSet
            .Setup(m => m.AddAsync(It.IsAny<SchedulerTaskInternalAttendee>(), default))
            .Callback<SchedulerTaskInternalAttendee, CancellationToken>((s, _) =>
            {
              s.SchedulerTaskInternalAttendeeId = data.Count + 1;
              data.Add(s);
            });

          schedulerTaskInternalAttendeeMockSet.Setup(m => m.Remove(It.IsAny<SchedulerTaskInternalAttendee>()))
            .Callback<SchedulerTaskInternalAttendee>(s =>
            {
              data.Remove(data.Find(t => t.SchedulerTaskInternalAttendeeId == s.SchedulerTaskInternalAttendeeId));
            });

          schedulerTaskInternalAttendeeMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var schedulerTaskInternalAttendee = schedulerTaskInternalAttendeeMockSet.Object
              .FirstOrDefaultAsync(b => b.SchedulerTaskInternalAttendeeId == (int)r[0]);
              return new ValueTask<SchedulerTaskInternalAttendee>(schedulerTaskInternalAttendee);
            });

          return schedulerTaskInternalAttendeeMockSet;
        }

        public static Mock<DbSet<SchedulerTaskReferralSource>> GetSchedulerTaskReferralSourceMockSet()
        {
          var data = new List<SchedulerTaskReferralSource>
          {
            new()
            {
              SchedulerTaskReferralSourceId = 1,
              SchedulerTaskId = 1,
              OrganizationId = 1,
              ReferralSourceId = 1,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null,
              ReferralSource = new ReferralSource
              {
                ReferralSourceId = 1,
                FirstName = "John",
                LastName = "Doe",
                Title = "Test title",
                Salutation = new Salutation
                {
                  SalutationId = 1,
                  Name = "Dr.",
                }
              },
              Organization = new Organization
              {
                OrganizationId = 1,
                Name = "Organization name"
              }
            }
          };

          var schedulerTaskReferralSourceMockSet = CreateDbSetMock(data.AsQueryable());
          schedulerTaskReferralSourceMockSet
            .Setup(m => m.AddAsync(It.IsAny<SchedulerTaskReferralSource>(), default))
            .Callback<SchedulerTaskReferralSource, CancellationToken>((s, _) =>
            {
              s.SchedulerTaskReferralSourceId = data.Count + 1;
              data.Add(s);
            });

          schedulerTaskReferralSourceMockSet.Setup(m => m.Remove(It.IsAny<SchedulerTaskReferralSource>()))
            .Callback<SchedulerTaskReferralSource>(s =>
            {
              data.Remove(data.Find(t => t.SchedulerTaskReferralSourceId == s.SchedulerTaskReferralSourceId));
            });

          schedulerTaskReferralSourceMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var schedulerTaskReferralSource = schedulerTaskReferralSourceMockSet.Object
              .FirstOrDefaultAsync(b => b.SchedulerTaskReferralSourceId == (int)r[0]);
              return new ValueTask<SchedulerTaskReferralSource>(schedulerTaskReferralSource);
            });

          return schedulerTaskReferralSourceMockSet;
        }

        public static Mock<DbSet<Goal>> GetGoalMockSet()
        {
          var data = new List<Goal>
          {
            new()
            {
              GoalId = 1,
              GoalTypeId = 1,
              LocationId = 1,
              YearId = 1,
              MonthId = 1,
              Value = null,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null,
            }
          };

          var goalMockSet = CreateDbSetMock(data.AsQueryable());
          goalMockSet
            .Setup(m => m.AddAsync(It.IsAny<Goal>(), default))
            .Callback<Goal, CancellationToken>((s, _) =>
            {
              s.GoalId = data.Count + 1;
              data.Add(s);
            });

          goalMockSet.Setup(m => m.Remove(It.IsAny<Goal>()))
            .Callback<Goal>(s =>
            {
              data.Remove(data.Find(t => t.GoalId == s.GoalId));
            });

          goalMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var goal = goalMockSet.Object
              .FirstOrDefaultAsync(b => b.GoalId == (int)r[0]);
              return new ValueTask<Goal>(goal);
            });

          return goalMockSet;
        }

        public static Mock<DbSet<ReferralSourceSpendingLimit>> GetReferralSourceSpendingLimitMockSet()
        {
          var data = new List<ReferralSourceSpendingLimit>
          {
            new()
            {
              ReferralSourceSpendingLimitId = 1,
              YearNo = 2023,
              NonMonetaryCompensationLimit = 1000,
              IncidentalBenefitLimit = 500,
              RemunerationLimit = 2000,
              Cpiuchange = 1.5m,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null,
            }
          };

          var referralSourceSpendingLimitMockSet = CreateDbSetMock(data.AsQueryable());
          referralSourceSpendingLimitMockSet
            .Setup(m => m.AddAsync(It.IsAny<ReferralSourceSpendingLimit>(), default))
            .Callback<ReferralSourceSpendingLimit, CancellationToken>((s, _) =>
            {
              s.ReferralSourceSpendingLimitId = data.Count + 1;
              data.Add(s);
            });

          referralSourceSpendingLimitMockSet.Setup(m => m.Remove(It.IsAny<ReferralSourceSpendingLimit>()))
            .Callback<ReferralSourceSpendingLimit>(s =>
            {
              data.Remove(data.Find(t => t.ReferralSourceSpendingLimitId == s.ReferralSourceSpendingLimitId));
            });

          referralSourceSpendingLimitMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var referralSourceSpendingLimit = referralSourceSpendingLimitMockSet.Object
              .FirstOrDefaultAsync(b => b.ReferralSourceSpendingLimitId == (int)r[0]);
              return new ValueTask<ReferralSourceSpendingLimit>(referralSourceSpendingLimit);
            });

          return referralSourceSpendingLimitMockSet;
        }

        public static Mock<DbSet<Payor>> GetPayorMockSet()
        {
          var data = new List<Payor>
          {
            new()
            {
              PayorId = 1,
              Name = "MyPayorA",
              PayorTypeId = 1,
              Disabled = false,
              Address = "4232 Kiowa Dr",
              City = "Carrollton",
              ElectronicPayorId = null,
              Fax = null,
              PayorMasterNumber = "123",
              Phone = "12345",
              PlanNumber = "12345",
              SsipayorId = null,
              ShortName = "MyPayorA",
              State = "TX",
              Zip = "TX",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null,
            }
          };

          var payorMockSet = CreateDbSetMock(data.AsQueryable());
          payorMockSet
            .Setup(m => m.AddAsync(It.IsAny<Payor>(), default))
            .Callback<Payor, CancellationToken>((s, _) =>
            {
              s.PayorId = data.Count + 1;
              data.Add(s);
            });

          payorMockSet.Setup(m => m.Remove(It.IsAny<Payor>()))
            .Callback<Payor>(s =>
            {
              data.Remove(data.Find(t => t.PayorId == s.PayorId));
            });

          payorMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var payor = payorMockSet.Object
              .FirstOrDefaultAsync(b => b.PayorId == (int)r[0]);
              return new ValueTask<Payor>(payor);
            });

          return payorMockSet;
        }

        public static Mock<DbSet<PayorLocation>> GetPayorLocationMockSet()
        {
          var data = new List<PayorLocation>();
          var payorLocationMockSet = CreateDbSetMock(data.AsQueryable());
          payorLocationMockSet
            .Setup(m => m.AddAsync(It.IsAny<PayorLocation>(), default))
            .Callback<PayorLocation, CancellationToken>((s, _) =>
            {
              s.Id = data.Count + 1;
              data.Add(s);
            });

          payorLocationMockSet.Setup(m => m.Remove(It.IsAny<PayorLocation>()))
            .Callback<PayorLocation>(s =>
            {
              data.Remove(data.Find(t => t.Id == s.Id));
            });

          payorLocationMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var payorLocation = payorLocationMockSet.Object
              .FirstOrDefaultAsync(b => b.Id == (int)r[0]);
              return new ValueTask<PayorLocation>(payorLocation);
            });

          return payorLocationMockSet;
        }

        public static Mock<DbSet<Diagnosis>> GetDiagnosisMockSet()
        {
          var data = new List<Diagnosis>
          {
            new()
            {
              DiagnosisId = 1,
              Name = "Stroke",
              CmsQualifying = true,
              ParentDiagnosisId = null,
              Disabled = false,
              MappingCode = null,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null
            }
          };

          var diagnosisMockSet = CreateDbSetMock(data.AsQueryable());
          diagnosisMockSet
            .Setup(m => m.AddAsync(It.IsAny<Diagnosis>(), default))
            .Callback<Diagnosis, CancellationToken>((s, _) =>
            {
              s.DiagnosisId = data.Count + 1;
              data.Add(s);
            });

          diagnosisMockSet.Setup(m => m.Remove(It.IsAny<Diagnosis>()))
            .Callback<Diagnosis>(s =>
            {
              data.Remove(data.Find(t => t.DiagnosisId == s.DiagnosisId));
            });

          diagnosisMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var diagnosis = diagnosisMockSet.Object
              .FirstOrDefaultAsync(b => b.DiagnosisId == (int)r[0]);
              return new ValueTask<Diagnosis>(diagnosis);
            });

          return diagnosisMockSet;
        }

        public static Mock<DbSet<AppLog>> GetAppLogMockSet()
        {
          var data = new List<AppLog>
          {
            new()
            {
              AppLogId = 1,
              ApplicationName = "Optima.Api",
              Host = "LAPTOP-LO2BJI30",
              Type = "System.InvalidOperationException",
              Source = "Microsoft.Data.SqlClient",
              Message = "Timeout expired.",
              IsError = true,
              Severity = "Error",
              DetailJson = "",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM",
              Deleted  = null
            }
          };

          var appLogMockSet = CreateDbSetMock(data.AsQueryable());
          appLogMockSet
            .Setup(m => m.AddAsync(It.IsAny<AppLog>(), default))
            .Callback<AppLog, CancellationToken>((s, _) =>
            {
              s.AppLogId = data.Count + 1;
              data.Add(s);
            });

          appLogMockSet.Setup(m => m.Remove(It.IsAny<AppLog>()))
            .Callback<AppLog>(s =>
            {
              data.Remove(data.Find(t => t.AppLogId == s.AppLogId));
            });

          appLogMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var appLog = appLogMockSet.Object
              .FirstOrDefaultAsync(b => b.AppLogId == (int)r[0]);
              return new ValueTask<AppLog>(appLog);
            });

          return appLogMockSet;
        }

        public static Mock<DbSet<AuditLog>> GetAuditLogMockSet()
        {
          var data = new List<AuditLog>
          {
            new()
            {
              AuditLogId = 1,
              Id = 0,
              OriginalValue = null,
              NewValue = null,
              Comments = "Referral Viewed",
              Entity = "Referral",
              LocationId = null,
              AuditType = "Viewed",
              Url = "/referral-dashboard",
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM"
            }
          };

          var auditLogMockSet = CreateDbSetMock(data.AsQueryable());
          auditLogMockSet
            .Setup(m => m.AddAsync(It.IsAny<AuditLog>(), default))
            .Callback<AuditLog, CancellationToken>((s, _) =>
            {
              s.AuditLogId = data.Count + 1;
              data.Add(s);
            });

          auditLogMockSet.Setup(m => m.Remove(It.IsAny<AuditLog>()))
            .Callback<AuditLog>(s =>
            {
              data.Remove(data.Find(t => t.AuditLogId == s.AuditLogId));
            });

          auditLogMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var auditLog = auditLogMockSet.Object
              .FirstOrDefaultAsync(b => b.AuditLogId == (int)r[0]);
              return new ValueTask<AuditLog>(auditLog);
            });

          return auditLogMockSet;
        }

        public static Mock<DbSet<Lesson>> GetLessonMockSet()
        {
          var data = new List<Lesson>
          {
            new()
            {
              LessonId = 1,
              Name = "Test Lession 1",
              EmbedHtml = "Test Lession 1 EmbedHtml",
              Archived = false,
              Deleted = null,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM"
            }
          };

          var lessonMockSet = CreateDbSetMock(data.AsQueryable());
          lessonMockSet
            .Setup(m => m.AddAsync(It.IsAny<Lesson>(), default))
            .Callback<Lesson, CancellationToken>((s, _) =>
            {
              s.LessonId = data.Count + 1;
              data.Add(s);
            });

          lessonMockSet.Setup(m => m.Remove(It.IsAny<Lesson>()))
            .Callback<Lesson>(s =>
            {
              data.Remove(data.Find(t => t.LessonId == s.LessonId));
            });

          lessonMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var lesson = lessonMockSet.Object
              .FirstOrDefaultAsync(b => b.LessonId == (int)r[0]);
              return new ValueTask<Lesson>(lesson);
            });

          return lessonMockSet;
        }

        public static Mock<DbSet<ZipCode>> GetZipCodeMockSet()
        {
          var data = new List<ZipCode>
          {
            new()
            {
              ZipCodeId = 1,
              LocationId = 6,
              ZipCode1 = "00501",
              ZipType = "UNIQUE",
              City = "Holtsville",
              State = "NY",
              County = "Suffolk County",
              TimeZone = "America/New_York",
              AreaCodes = "631",
              Country = "US",
              Lattitude = 40.810000000M,
              Longitude = -73.040000000M,
              Disabled = false,
              Created = new DateTime(),
              CreatedBy = "SYSTEM",
              Updated = new DateTime(),
              UpdatedBy = "SYSTEM"
            }
          };

          var zipCodeMockSet = CreateDbSetMock(data.AsQueryable());
          zipCodeMockSet
            .Setup(m => m.AddAsync(It.IsAny<ZipCode>(), default))
            .Callback<ZipCode, CancellationToken>((s, _) =>
            {
              s.ZipCodeId = data.Count + 1;
              data.Add(s);
            });

          zipCodeMockSet.Setup(m => m.Remove(It.IsAny<ZipCode>()))
            .Callback<ZipCode>(s =>
            {
              data.Remove(data.Find(t => t.ZipCodeId == s.ZipCodeId));
            });

          zipCodeMockSet
            .Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns((object[] r) =>
            {
              var zipCode = zipCodeMockSet.Object
              .FirstOrDefaultAsync(b => b.ZipCodeId == (int)r[0]);
              return new ValueTask<ZipCode>(zipCode);
            });

          return zipCodeMockSet;
        }*/

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> items) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IAsyncEnumerable<T>>()
              .Setup(x => x.GetAsyncEnumerator(default))
              .Returns(new TestAsyncEnumerator<T>(items.GetEnumerator()));
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.Provider)
              .Returns(new TestAsyncQueryProvider<T>(items.Provider));
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.Expression).Returns(items.Expression);
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.ElementType).Returns(items.ElementType);
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            return dbSetMock;
        }
    }
}
