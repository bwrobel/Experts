using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using C5;
using Experts.Core.Entities;
using Experts.Web.Filters;
using Experts.Core.Utils;
using Experts.Core.Entities.Events;
using Experts.Web.Helpers;

namespace Experts.Web.Controllers
{
    [OnlyDeveloper]
    public partial class DevelopmentController : BaseController
    {
        [DefaultRouting]
        public virtual ActionResult Exception()
        {
            throw new NotImplementedException("This is a test system failure");
        }

        [DefaultRouting]
        public virtual ActionResult RefreshDataFresh()
        {
            DatabaseUtil.RefreshDataFresh();
            return Content(DateTime.Now.ToString());
        }

        [DefaultRouting]
        public virtual ActionResult LastError()
        {
            var lastErrorEvent = Repository.Event.GetLastSystemFailure();
            return lastErrorEvent != null ? Content(lastErrorEvent.Data) : null;
        }

        [DefaultRouting]
        public virtual ActionResult GenerateData()
        {
            GenerateCategoriesStructure();
            GenerateProvisions();
            GenerateUsers();
            GenerateUserOpinions();
            GenerateExpertOpinions();
            GenerateBDDTestingData();

            DatabaseUtil.PrepareDataFresh();

            return Content(DateTime.Now.ToString());
        }

        private void GenerateProvisions()
        {
            var expertProvisionNovice = new Provision
            {
                Description = "Starter provision for novice experts",
                ProvisionExpert = ProvisionExpert.Novice,
                ProvisionValue = 0.65M
            };
            Repository.Provision.Add(expertProvisionNovice);

            var expertProvisionIntermediate = new Provision
            {
                Description = "Better provision for intermediate experts",
                ProvisionExpert = ProvisionExpert.Intermediate,
                ProvisionValue = 0.75M
            };
            Repository.Provision.Add(expertProvisionIntermediate);

            var expertProvisionExperienced = new Provision
            {
                Description = "Advanced provision for experienced experts",
                ProvisionExpert = ProvisionExpert.Experienced,
                ProvisionValue = 0.85M
            };
            Repository.Provision.Add(expertProvisionExperienced);

            var expertProvisionFullProfit = new Provision
            {
                Description = "Full profit provision for best experts",
                ProvisionExpert = ProvisionExpert.FullProfit,
                ProvisionValue = 0.95M
            };
            Repository.Provision.Add(expertProvisionFullProfit);

            var partnerProvisionStarter = new Provision
            {
                Description = "Starter provision for partners",
                ProvisionPartner = ProvisionPartner.StandardPartner,
                ProvisionValue = 0.2M
            };
            Repository.Provision.Add(partnerProvisionStarter);
        }

        private void GenerateBDDTestingData()
        {
            //generating users
            var userSalt = CryptoHelper.CreateSalt();

            var user = new User
            {
                Email = "user-asknuts@asknuts.com",
                IsActivated = true,
                Password = "haslo1",
                PasswordSalt = userSalt,
                BankAccountNumber = "654510157739999994584019961664"
            };

            Repository.User.Add(user);
            Repository.User.AddEmailConfigurationDefaultValue(user);

            var moderator = new Moderator
            {
                PublicName = "Szalony Moderator",

                User = new User
                {
                    Email = "moderator-asknuts@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "116910157739999994584019961664"
                }
            };

            Repository.User.Add(moderator.User);
            Repository.Moderator.Add(moderator);
            Repository.User.AddEmailConfigurationDefaultValue(moderator.User);

            var expert1 = new Expert
            {
                FirstName = "test-name",
                LastName = "test-surname",
                PublicName = "test-expert",
                PhoneNumber = "671707525",
                VerificationStatus = ExpertVerificationStatus.Verified,
                User = new User
                {
                    Email = "expert-asknuts@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "204510157739991764584019961664"
                }
            };

            foreach (var category in Repository.Category.All())
            {
                expert1.Categories.Add(category);
                expert1.CategoryAttributes.Add(new ExpertCategoryAttributeValues
                                                    {
                                                        Category = category,
                                                        ExpertReason = category.Name + " dfuq am i doing here"}
                                                    );
            }

            expert1.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "test-desc",
                Position = "test-position"
            });

            Repository.User.Add(expert1.User);
            Repository.Expert.Add(expert1);
            Repository.User.AddEmailConfigurationDefaultValue(expert1.User);
            Repository.Expert.AssignProvisionToExpert(expert1, ProvisionExpert.Novice);

            Repository.Partner.Create(expert1.User);

            var expert2 = new Expert
            {
                FirstName = "test-name2",
                LastName = "test-surname2",
                PublicName = "test-expert2",
                PhoneNumber = "271707325",
                VerificationStatus = ExpertVerificationStatus.Verified,
                User = new User
                {
                    Email = "expert-asknuts2@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "404510157739991344584019961664"
                }
            };

            foreach (var category in Repository.Category.All())
            {
                expert2.Categories.Add(category);
                expert2.CategoryAttributes.Add(new ExpertCategoryAttributeValues
                {
                    Category = category,
                    ExpertReason = category.Name + " dfuq am i doing here 2"
                }
                                                    );
            }

            expert2.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "test-desc2",
                Position = "test-position2"
            });

            Repository.User.Add(expert2.User);
            Repository.Expert.Add(expert2);
            Repository.User.AddEmailConfigurationDefaultValue(expert2.User);
            Repository.Expert.AssignProvisionToExpert(expert2, ProvisionExpert.Novice);

            Repository.Partner.Create(expert2.User);

            //generating threads
            
            //zadane, opłacone pytanie (user-asknuts@asknuts.com) bez eksperta
            var thread1 = new Thread
                             {
                                 Priority = ThreadPriority.Medium,
                                 Verbosity = ThreadVerbosity.Low,
                                 State = ThreadState.Unoccupied,
                                 SanitizationStatus = ThreadSanitizationStatus.NotSanitized,
                                 Value = 5.00m,
                                 IsPaid = true,
                                 Author = user,
                                 Posts = new List<Post>{new Post
                                                            {
                                                                Content = "Pytanie testowe bez eksperta",
                                                                CreationDate = DateTime.Now.AddDays(-2),
                                                                LastModificationDate = DateTime.Now.AddDays(-2),
                                                                Type = PostType.Question,
                                                                Author = user
                                                            }
                                 },
                                 Category = Repository.Category.Find(c => c.Name == "Nauka")
                             };

            thread1.PriceProposals.Add(new PriceProposal
                                           {
                                               Comment = "Proponuje wyższą cenę",
                                               CreationDate = DateTime.Now.AddDays(-1),
                                               LastModificationDate = DateTime.Now.AddDays(-1),
                                               Expert = expert2,
                                               SurchargeValue = 10,
                                               VerificationStatus = PriceProposalVerificationStatus.Verified
                                           });

            Repository.Thread.Add(thread1);

            thread1.CreationDate = DateTime.Now.AddDays(-2);
            Repository.Thread.Update(thread1);

            //zadane, opłacone pytanie z autorem (user-asknuts@asknuts.com) z ekspertem (expert-asknuts@asknuts.com)
            var thread2 = new Thread
                              {
                                  Priority = ThreadPriority.Medium,
                                  Verbosity = ThreadVerbosity.Low,
                                  State = ThreadState.Occupied,
                                  SanitizationStatus = ThreadSanitizationStatus.NotSanitized,
                                  Value = 5.00m,
                                  IsPaid = true,
                                  Author = user,
                                  Expert = expert1,
                                  Posts = new List<Post>
                                              {
                                                  new Post
                                                      {
                                                          Content = "Pytanie testowe z ekspertem",
                                                          Type = PostType.Question,
                                                          Author = user
                                                      },
                                                  new Post
                                                      {
                                                          Content = "Odpowiedź testowa",
                                                          Type = PostType.Answer,
                                                          Author = expert1.User
                                                      }
                                              },
                                  Category = Repository.Category.Find(c => c.Name == "Nauka")
                              };

            Repository.Thread.Add(thread2);

            // zadane, nieopłacone pytanie
            var thread3 = new Thread
                              {
                                  Priority = ThreadPriority.Medium,
                                  Verbosity = ThreadVerbosity.High,
                                  State = ThreadState.Unoccupied,
                                  SanitizationStatus = ThreadSanitizationStatus.NotSanitized,
                                  Value = 20.00m,
                                  IsPaid = false,
                                  Author = user,
                                  Posts = new List<Post>
                                              {
                                                  new Post
                                                      {
                                                          Content = "Nieopłacone pytanie testowe",
                                                          Type = PostType.Question,
                                                          Author = user
                                                      }
                                              },
                                  Category = Repository.Category.Find(c => c.Name == "Nauka")
                              };

            Repository.Thread.Add(thread3);

            var thread4 = new Thread
                              {
                                  Priority = ThreadPriority.Low,
                                  Verbosity = ThreadVerbosity.Medium,
                                  State = ThreadState.Closed,
                                  SanitizationStatus = ThreadSanitizationStatus.NotSanitized,
                                  Value = 10.00m,
                                  IsPaid = true,
                                  Author = user,
                                  Posts = new List<Post>
                                              {
                                                  new Post
                                                      {
                                                          Content = "Zamknięte pytanie testowe",
                                                          Type = PostType.Question,
                                                          Author = user
                                                      },
                                                  new Post
                                                      {
                                                          Content = "Odpowiedź testowa",
                                                          Type = PostType.Answer,
                                                          Author = expert1.User
                                                      }
                                              },
                                  Category = Repository.Category.Find(c => c.Name == "Nauka")
                              };

            Repository.Thread.Add(thread4);

            //zamkniete i oczyszczone pytanie
            var thread5 = new Thread
                              {
                                  Priority = ThreadPriority.Low,
                                  Verbosity = ThreadVerbosity.Medium,
                                  State = ThreadState.Closed,
                                  SanitizationStatus = ThreadSanitizationStatus.Sanitized,
                                  Value = 10.00m,
                                  IsPaid = true,
                                  Author = user,
                                  Expert = expert1,
                                  Posts = new List<Post>
                                              {
                                                  new Post
                                                      {
                                                          Content = "Zamknięte pytanie testowe sanitized",
                                                          Type = PostType.Question,
                                                          Author = user
                                                      },
                                                  new Post
                                                      {
                                                          Content = "Odpowiedź testowa sanitized",
                                                          Type = PostType.Answer,
                                                          Author = expert1.User
                                                      }
                                              },
                                  Category = Repository.Category.Find(c => c.Name == "Nauka")
                              };
            Repository.Thread.Add(thread5);

            /* generating SEO keywords */
            var seoKeyword1 = new SEOKeyword
                                 {
                                     Category = Repository.Category.Find(c => c.Name == "Nauka"),
                                     Phrase = "ekspert",
                                     Type = SEOKeywordType.Expert,
                                     Status = SEOKeywordStatus.Processed,
                                     Source = SEOKeywordSource.Automatic
                                 };
            Repository.SEOKeyword.Add(seoKeyword1);

            var seoKeyword2 = new SEOKeyword
            {
                Category = Repository.Category.Find(c => c.Name == "Nauka"),
                Phrase = "fraza",
                Type = SEOKeywordType.Phrase,
                Status = SEOKeywordStatus.Processed,
                Source = SEOKeywordSource.Automatic
            };
            Repository.SEOKeyword.Add(seoKeyword2);

            var seoKeyword3 = new SEOKeyword
            {
                Category = Repository.Category.Find(c => c.Name == "Nauka"),
                Phrase = "pytanie",
                Type = SEOKeywordType.Question,
                Status = SEOKeywordStatus.Processed,
                Source = SEOKeywordSource.Automatic
            };
            Repository.SEOKeyword.Add(seoKeyword3);
        }

        private void GenerateUsers()
        {
            var userSalt = CryptoHelper.CreateSalt();
            
            //Asknuts user

            var asknutsConsultant = new Consultant
            {
                PublicName = "Konsultantka Asknuts",

                User = new User
                {
                    Email = "consultant-asknuts@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                }
            };

            Repository.User.Add(asknutsConsultant.User);
            Repository.Consultant.Add(asknutsConsultant);
            Repository.User.AddEmailConfigurationDefaultValue(asknutsConsultant.User);

            //Experts to specific categories

            var lawExpert = new Expert
            {
                FirstName = "Piotr",
                LastName = "Michalski",
                PublicName = "Piotr Michalski",
                PhoneNumber = "671707525",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Prawo i podatki"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "prawoipodatki@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "201960157739011764584019961664"
                }
            };

            lawExpert.Categories.Add(Repository.Category.Get(1));
            Repository.Expert.AssignProvisionToExpert(lawExpert, ProvisionExpert.Novice);
            lawExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Po­sia­dam sze­ro­kie do­świad­cze­nie w za­kre­sie pra­wa po­dat­ko­we­go, w szcze­gól­no­ści w za­kre­sie po­stę­po­wa­nia po­dat­ko­we­go oraz są­do­wo­–ad­mi­ni­stra­cyj­ne­go. Z po­wo­dze­niem re­pre­zen­to­wa­łem klien­tów w spra­wach po­dat­ko­wych przed or­ga­na­mi skar­bo­wy­mi i są­da­mi ad­mi­ni­stra­cyj­ny­mi.",
                Position = "Doradca podatkowy"
            });

            Repository.User.Add(lawExpert.User);
            Repository.Expert.Add(lawExpert);
            Repository.User.AddEmailConfigurationDefaultValue(lawExpert.User);

            //

            var businessExpert = new Expert
            {
                FirstName = "Gabriela",
                LastName = "Woźniak",
                PublicName = "Gabriela Woźniak",
                PhoneNumber = "518266333",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Biznes i finanse"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "biznesifinanse@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "901970157739911764582019961264"
                }
            };

            businessExpert.Categories.Add(Repository.Category.Get(2));
            Repository.Expert.AssignProvisionToExpert(businessExpert, ProvisionExpert.Intermediate);
            businessExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "­­Cześć! Ukończyłam kierunek Finanse na Uniwersytecie Ekonomicznym w Katowicach. Od tego czasu pracuję i zdobywam doświadczenie w różnych bankach. Obecnie rozwijam się i pracuje w ING Bank Śląski. Jeżeli masz jakiekolwiek pytania z dziedziny finansów/inwestycji lub szerokopojętego biznesu to pisz, a ja postaram się na nie odpowiedzieć.",
                Position = "Konsultant finansowy"
            });

            Repository.User.Add(businessExpert.User);
            Repository.Expert.Add(businessExpert);
            Repository.User.AddEmailConfigurationDefaultValue(businessExpert.User);

            //

            var animalsExperts = new Expert
            {
                FirstName = "Natasza",
                LastName = "Wysocka",
                PublicName = "Natasza Wysocka",
                PhoneNumber = "538484862",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Zwierzęta i weterynaria"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "zwierzetaiweterynaria@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "301273157741911764582019961262"
                }
            };

            animalsExperts.Categories.Add(Repository.Category.Get(3));
            Repository.Expert.AssignProvisionToExpert(animalsExperts, ProvisionExpert.Intermediate);
            animalsExperts.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "­­Ponad 15 lat pracuję w branży weterynaryjnej. Moje bogate doświadczenie zdobyłam pracując w licznych schroniskach i przychodniach weterynaryjnych. Skończyłam kierunek zootechniki na SGGW w Warszawie.",
                Position = "Weterynarz"
            });

            Repository.User.Add(animalsExperts.User);
            Repository.Expert.Add(animalsExperts);
            Repository.User.AddEmailConfigurationDefaultValue(animalsExperts.User);

            //

            var travelsExpert = new Expert
            {
                FirstName = "Radosław",
                LastName = "Jasiński",
                PublicName = "Radosław Jasiński",
                PhoneNumber = "528412362",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Podróże"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "podroze@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "92223234741911654582019961902"
                }
            };

            travelsExpert.Categories.Add(Repository.Category.Get(4));
            Repository.Expert.AssignProvisionToExpert(travelsExpert, ProvisionExpert.Intermediate);
            travelsExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "­Od 10 lat zajmuję się profesjonalną organizacją ekstrymalnych wypraw turystycznych w różne rejony świata, od deszczowych lasów Amazonii po wysokie góry pasa Himalajów.",
                Position = ""
            });

            Repository.User.Add(travelsExpert.User);
            Repository.Expert.Add(travelsExpert);
            Repository.User.AddEmailConfigurationDefaultValue(travelsExpert.User);

            //

            var entertainmentExpert = new Expert
            {
                FirstName = "Zbigniew",
                LastName = "Pawlak",
                PublicName = "Zbigniew Pawlak",
                PhoneNumber = "514087935",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Rozrywka i rekreacja"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "rozrywkairekreacjae@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "35223234741964524582019961931"
                }
            };

            entertainmentExpert.Categories.Add(Repository.Category.Get(5));
            Repository.Expert.AssignProvisionToExpert(entertainmentExpert, ProvisionExpert.Novice);
            entertainmentExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Jestem kinomanem od dzieciństwa. Razem z przyjacielem prowadzę serwis filmowy w którym od ponad 5 lat zamieszczane są recenzje, opinie oraz krytyka filmowa. Kino, szczególnie amerykańskie z lat 80 to moja pasja.",
                Position = ""
            });

            Repository.User.Add(entertainmentExpert.User);
            Repository.Expert.Add(entertainmentExpert);
            Repository.User.AddEmailConfigurationDefaultValue(entertainmentExpert.User);

            //

            var cookeryExpert = new Expert
            {
                FirstName = "Zdzisław",
                LastName = "Jabłoński",
                PublicName = "Zdzisław Jabłoński",
                PhoneNumber = "531040758",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Sztuka kulinarna"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "sztukakulinarna@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "96233234742654524582019961931"
                }
            };

            cookeryExpert.Categories.Add(Repository.Category.Get(6));
            Repository.Expert.AssignProvisionToExpert(cookeryExpert, ProvisionExpert.Novice);
            cookeryExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Obecnie pracuję jako szef kuchnii w Warszawskiej sieci restauracji. Skończyłem studia na kierunku Gastronomia i sztuka kulinarna. Pracę w zawodzie zaacząłem już na studiach co pozwoliło mi na zdobycie cennego doświadczenia.",
                Position = "Szef kuchnii"
            });

            Repository.User.Add(cookeryExpert.User);
            Repository.Expert.Add(cookeryExpert);
            Repository.User.AddEmailConfigurationDefaultValue(cookeryExpert.User);

            //

            var childrenExpert = new Expert
            {
                FirstName = "Weronika",
                LastName = "Piotrowska",
                PublicName = "Weronika Piotrowska",
                PhoneNumber = "532754932",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Dzieci"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "dzieci@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "12393234742654524582019964561"
                }
            };

            childrenExpert.Categories.Add(Repository.Category.Get(7));
            Repository.Expert.AssignProvisionToExpert(childrenExpert, ProvisionExpert.Novice);
            childrenExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Skończyłam studia doktorackie na kierunku Pedagogika na Uniwersytecie Warszawskim. Moje powołanie do pracy z dziećmi odkryłam w szkole średniej, gdzie organizowane były różne akcje charytatywne. Obecnie pracuje jako wykładowca na tej samej uczelni.",
                Position = "Pedagog"
            });

            Repository.User.Add(childrenExpert.User);
            Repository.Expert.Add(childrenExpert);
            Repository.User.AddEmailConfigurationDefaultValue(childrenExpert.User);

            //

            var eventsExpert = new Expert
            {
                FirstName = "Stefania",
                LastName = "Kaczmarek",
                PublicName = "Stefania Kaczmarek",
                PhoneNumber = "535529051",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Wydarzenia i ceremonie"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "wydarzeniaiceremonie@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "65953234742654524124519964561"
                }
            };

            eventsExpert.Categories.Add(Repository.Category.Get(8));
            Repository.Expert.AssignProvisionToExpert(eventsExpert, ProvisionExpert.Novice);
            eventsExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Prowadzę własną firmę organizującą rozmaite uroczystości (np. Wesela/Komunie/Koncerty). Posiadam 10 letnie doświadczenie w organizowaniu róznych imprez.",
                Position = ""
            });

            Repository.User.Add(eventsExpert.User);
            Repository.Expert.Add(eventsExpert);
            Repository.User.AddEmailConfigurationDefaultValue(eventsExpert.User);

            //

            var aboutMeExpert = new Expert
            {
                FirstName = "Klara",
                LastName = "Gąska",
                PublicName = "Klara Gąska",
                PhoneNumber = "455451051",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Ja i o mnie"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "jaiomnie@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "65953234742654524124519964561"
                }
            };

            aboutMeExpert.Categories.Add(Repository.Category.Get(9));
            Repository.Expert.AssignProvisionToExpert(aboutMeExpert, ProvisionExpert.Experienced);
            aboutMeExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Praca z ludźmi to moja pasja. Po ukończonych studiach na kierunki Psychologii postanowiłam założyć własną firmę. Od ponad 12 lat zajmuję się rozwiązywaniem ludzkich problemów o podłożu psychologicznym i rozwojowym.",
                Position = "Psycholog"
            });

            Repository.User.Add(aboutMeExpert.User);
            Repository.Expert.Add(aboutMeExpert);
            Repository.User.AddEmailConfigurationDefaultValue(aboutMeExpert.User);

            //

            var loveExpert = new Expert
            {
                FirstName = "Bogumiła",
                LastName = "Nowakowska",
                PublicName = "Bogumiła Nowakowska",
                PhoneNumber = "965251454",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Miłość i relacje"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "miloscirelacje@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "11959655742654524124519964561"
                }
            };

            loveExpert.Categories.Add(Repository.Category.Get(10));
            Repository.Expert.AssignProvisionToExpert(loveExpert, ProvisionExpert.Novice);
            loveExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Od lat interesują mnie relacje międzyludzkie, dlatego po ukończeniu socjologii postanowiłam poświecić się temu powołaniu. Jeżeli masz jakiekolwiek pytania na temat relacji międzyludzkich i problemów o naturze socjalnej to śmiało pytaj!",
                Position = "Socjolog"
            });

            Repository.User.Add(loveExpert.User);
            Repository.Expert.Add(loveExpert);
            Repository.User.AddEmailConfigurationDefaultValue(loveExpert.User);

            //

            var scienceExpert = new Expert
            {
                FirstName = "Michał",
                LastName = "Kowalczyk",
                PublicName = "Michał Kowalczyk",
                PhoneNumber = "662251454",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Nauka"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "nauka@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "33359655742654524914519964561"
                }
            };

            scienceExpert.Categories.Add(Repository.Category.Get(11));
            Repository.Expert.AssignProvisionToExpert(scienceExpert, ProvisionExpert.Intermediate);
            scienceExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "",
                Position = "Nauczyciel Matematyki"
            });

            Repository.User.Add(scienceExpert.User);
            Repository.Expert.Add(scienceExpert);
            Repository.User.AddEmailConfigurationDefaultValue(scienceExpert.User);

            //

            var internetExpert = new Expert
            {
                FirstName = "Jacek",
                LastName = "Małatuński",
                PublicName = "Jacek Małatuński",
                PhoneNumber = "222251454",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Komputery i internet"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "komputeryiinternet@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "33359655742654524914519964561"
                }
            };

            internetExpert.Categories.Add(Repository.Category.Get(12));
            Repository.Expert.AssignProvisionToExpert(internetExpert, ProvisionExpert.Intermediate);
            internetExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "",
                Position = "Informatyk"
            });

            Repository.User.Add(internetExpert.User);
            Repository.Expert.Add(internetExpert);
            Repository.User.AddEmailConfigurationDefaultValue(internetExpert.User);

            //

            var houseExpert = new Expert
            {
                FirstName = "Damian",
                LastName = "Izomorski",
                PublicName = "Damian Izomorski",
                PhoneNumber = "599951454",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Dom"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "dom@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "33359655742622124914519964561"
                }
            };

            houseExpert.Categories.Add(Repository.Category.Get(13));
            Repository.Expert.AssignProvisionToExpert(houseExpert, ProvisionExpert.Intermediate);
            houseExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Ukończyłem studia na kierunku Architektury Wnętrz. Projektowaniem domów i wnętrz zajmuję się od 15 lat.",
                Position = "Projektant"
            });

            Repository.User.Add(houseExpert.User);
            Repository.Expert.Add(houseExpert);
            Repository.User.AddEmailConfigurationDefaultValue(houseExpert.User);

            //

            var equipmentExpert = new Expert
            {
                FirstName = "Szymon",
                LastName = "Czarnecki",
                PublicName = "Szymon Czarnecki",
                PhoneNumber = "599988864",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Sprzęt i naprawa"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "sprzetinaprawa@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "99563123452622124914519964561"
                }
            };

            equipmentExpert.Categories.Add(Repository.Category.Get(14));
            Repository.Expert.AssignProvisionToExpert(equipmentExpert, ProvisionExpert.Intermediate);
            equipmentExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Pracuję w serwisie napraw sprzętu znanej firmy. Od 5 lat zajume się ich naprawą.",
                Position = ""
            });

            Repository.User.Add(equipmentExpert.User);
            Repository.Expert.Add(equipmentExpert);
            Repository.User.AddEmailConfigurationDefaultValue(equipmentExpert.User);

            //

            var gardenExpert = new Expert
            {
                FirstName = "Dawid",
                LastName = "Czerwiński",
                PublicName = "Dawid Czerwiński",
                PhoneNumber = "512399464",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Ogród"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "ogrod@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "99563123452622124914519961221"
                }
            };

            gardenExpert.Categories.Add(Repository.Category.Get(15));
            Repository.Expert.AssignProvisionToExpert(gardenExpert, ProvisionExpert.Intermediate);
            gardenExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Projektowanie ogrodów to moja pasja od 10 lat. Poprawny dobór roślin, kwiatów i drzew to jeden z najważniejszych czynników przy budowie doskonałego ogrodu. Potrafię doradzić lub nawet zaprojektować cały ogród dla państwa.",
                Position = ""
            });

            Repository.User.Add(gardenExpert.User);
            Repository.Expert.Add(gardenExpert);
            Repository.User.AddEmailConfigurationDefaultValue(gardenExpert.User);

            //

            var artExpert = new Expert
            {
                FirstName = "Władysław",
                LastName = "Dąbrowski",
                PublicName = "Władysław Dąbrowski",
                PhoneNumber = "555965784",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Sztuka i kultura"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "sztukaikultura@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "99125952452622124914519961221"
                }
            };

            artExpert.Categories.Add(Repository.Category.Get(16));
            Repository.Expert.AssignProvisionToExpert(artExpert, ProvisionExpert.Intermediate);
            artExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Moją pasją od ponad 10 lat są kultury wschodu. W szczególności interesuję się kulturami azjatyckimi.",
                Position = ""
            });

            Repository.User.Add(artExpert.User);
            Repository.Expert.Add(artExpert);
            Repository.User.AddEmailConfigurationDefaultValue(artExpert.User);

            //

            var healthExpert = new Expert
            {
                FirstName = "Mieczysław",
                LastName = "Szymanski",
                PublicName = "Mieczysław Szymanski",
                PhoneNumber = "568585784",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Zdrowie i medycyna"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "zdrowieimedycyna@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "55542952452622124914519961221"
                }
            };

            healthExpert.Categories.Add(Repository.Category.Get(17));
            Repository.Expert.AssignProvisionToExpert(healthExpert, ProvisionExpert.Intermediate);
            healthExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Ukończyłem stomatologię na Uniwerystecie Medycznym w Poznaniu. Po zdobyciu 20-letniego doświadczenia w publicznych klinikach stomatologicznych, założyłem własną klinikę.",
                Position = "Stomatolog"
            });

            Repository.User.Add(healthExpert.User);
            Repository.Expert.Add(healthExpert);
            Repository.User.AddEmailConfigurationDefaultValue(healthExpert.User);

            //

            var autoExpert = new Expert
            {
                FirstName = "Jarosław",
                LastName = "Król",
                PublicName = "Jarosław Król",
                PhoneNumber = "561245784",
                VerificationStatus = ExpertVerificationStatus.Verified,
                VerificationDetails = @"Kompetencje eksperta z dziedziny ""Motoryzacja"" zostały zweryfikowane.",
                User = new User
                {
                    Email = "motoryzacja@asknuts.com",
                    IsActivated = true,
                    Password = "haslo1",
                    PasswordSalt = userSalt,
                    BankAccountNumber = "55124578452622124914519961221"
                }
            };

            autoExpert.Categories.Add(Repository.Category.Get(18));
            Repository.Expert.AssignProvisionToExpert(autoExpert, ProvisionExpert.Intermediate);
            autoExpert.Microprofiles.Add(new ExpertMicroprofile
            {
                Description = "Prowadzę własny warsztat samochodowy od 8 lat. Mechanikę ukończyłem na Politechnice Gliwickiej",
                Position = "Właściciel warsztatu samochodowego"
            });

            Repository.User.Add(autoExpert.User);
            Repository.Expert.Add(autoExpert);
            Repository.User.AddEmailConfigurationDefaultValue(autoExpert.User);

        }

        private void GenerateCategoriesStructure()
        {
            //law and taxes
            var lawAndTaxes = new Category
            {
                Name = "Prawo i podatki",
                Description = "Kategoria związana z zagadnieniami księgowości, prawa karnego, podatkowego, patentowego, autorskiego itp. oraz z zagadnieniami podatków.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 1
            };
            AddPrestigePriceSet(lawAndTaxes.Prices);
            AddLawAndTaxesAttributesSet(lawAndTaxes.Attributes);
            Repository.Category.Add(lawAndTaxes);

            //business and finance
            var businessAndFinance = new Category
            {
                Name = "Biznes i finanse",
                Description = "Inwestycje, kredyty, ubezpieczenia, własna firma oraz inne zagadnienia związane  z biznesem i finansami.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 2
            };
            AddPrestigePriceSet(businessAndFinance.Prices);
            AddBusinessAndFinanceAtrributesSet(businessAndFinance.Attributes);
            Repository.Category.Add(businessAndFinance);

            //animals and veterninary
            var animalsAndVeterninary = new Category
            {
                Name = "Zwierzęta i weterynaria",
                Description = "Psy, koty, rybki lub inne zwierzaki. Pielęgnacja, żywienie oraz choroby zwierzęce.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 7
            };
            AddPrestigePriceSet(animalsAndVeterninary.Prices);
            AddAnimalsAndVeterinaryAtrributesSet(animalsAndVeterninary.Attributes);
            Repository.Category.Add(animalsAndVeterninary);

            //travels
            var travels = new Category
            {
                Name = "Podróże",
                Description = "Ciepłe kraje, wysokie Himalaje oraz wszystkie inne zagadnienia związane z podróżami.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 8
            };
            AddStandardPriceSet(travels.Prices);
            AddTravelsAtrributesSet(travels.Attributes);
            Repository.Category.Add(travels);

            //entertainment and recreation
            var entertainmentAndRecreation = new Category
            {
                Name = "Rozrywka i rekreacja",
                Description = "Filmy, gry komputerowe, sport, muzyka oraz wszystkie inne zagadnienia związane z rozrywką i rekreacją.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 9
            };
            AddLowPriceSet(entertainmentAndRecreation.Prices);
            AddEntertainmentAndRecreationAtrributesSet(entertainmentAndRecreation.Attributes);
            Repository.Category.Add(entertainmentAndRecreation);

            //cookery
            var cookery = new Category
            {
                Name = "Sztuka kulinarna",
                Description = "Kuchnie świata, przepisy oraz diety.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 10
            };
            AddLowPriceSet(cookery.Prices);
            AddCookeryAtrributesSet(cookery.Attributes);
            Repository.Category.Add(cookery);

            //children
            var children = new Category
            {
                Name = "Dzieci",
                Description = "Wychowanie oraz rozwój dzieci.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 11
            };
            AddStandardPriceSet(children.Prices);
            AddChildrenAtrributesSet(children.Attributes);
            Repository.Category.Add(children);

            //events and ceremnonies
            var eventsAndCeremonies = new Category
            {
                Name = "Wydarzenia i ceremonie",
                Description = "Wesela, pogrzeby, urodziny oraz inne wydarzenia. Pytania związane z organizacją i nie tylko.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 12
            };
            AddLowPriceSet(eventsAndCeremonies.Prices);
            AddEventsAndCeremoniesAtrributesSet(eventsAndCeremonies.Attributes);
            Repository.Category.Add(eventsAndCeremonies);
            
            //about me
            var aboutMe = new Category
            {
                Name = "Ja i o mnie",
                Description = "Twoja własna osoba, charakter, wygląd lub inne zagadnienia związane z twoją personą.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 13
            };
            AddStandardPriceSet(aboutMe.Prices);
            AddAboutMeAtrributesSet(aboutMe.Attributes);
            Repository.Category.Add(aboutMe);

            //love and relations
            var loveAndRelations = new Category
            {
                Name = "Miłość i relacje",
                Description = "Relacje międzyludzkie, problemy miłosne.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 3
            };
            AddStandardPriceSet(loveAndRelations.Prices);
            AddLoveAndRelationsAtrributesSet(loveAndRelations.Attributes);
            Repository.Category.Add(loveAndRelations);

            //science
            var science = new Category
            {
                Name = "Nauka",
                Description = "Kategoria związana z naukami ścisłymi, humanistycznymi, przyrodniczymi, a także z językami obcymi.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 4
            };
            AddLowPriceSet(science.Prices);
            AddScienceAtrributesSet(science.Attributes);
            Repository.Category.Add(science);

            //internet and computers
            var internetAndComputers = new Category
            {
                Name = "Komputery i internet",
                Description = "Naprawa komputerów, wybór sprzętu i oprogramowania.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 5
            };
            AddLowPriceSet(internetAndComputers.Prices);
            AddInternetAndComputersAtrributesSet(internetAndComputers.Attributes);
            Repository.Category.Add(internetAndComputers);

            //house
            var house = new Category
            {
                Name = "Dom",
                Description = "Projektowanie, budowa, remont domu. Pytania związane z nieruchomościami, wystrojem wnętrz oraz innymi zagadnieniami związanymi z domem.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 14,
            };
            AddStandardPriceSet(house.Prices);
            AddHouseAtrributesSet(house.Attributes);
            Repository.Category.Add(house);

            //equipment repair
            var equipmentRepair = new Category
            {
                Name = "Sprzęt i naprawa",
                Description = "Naprawa oraz poprawne użytkowanie sprzętu.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 15
            };
            AddStandardPriceSet(equipmentRepair.Prices);
            AddEquipmentRepairAtrributesSet(equipmentRepair.Attributes);
            Repository.Category.Add(equipmentRepair);

            //garden
            var garden = new Category
            {
                Name = "Ogród",
                Description = "Kwiaty i drzewa. Projektowanie oraz pielęgnacja ogrodów.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 16
            };
            AddStandardPriceSet(garden.Prices);
            AddGardenAtrributesSet(garden.Attributes);
            Repository.Category.Add(garden);

            //art and culture
            var artAndCulture = new Category
            {
                Name = "Sztuka i kultura",
                Description = "Sławne obrazy, tradycje azjatycje oraz wszystkie inne zagadnienia związane z kulturą i sztuką.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 17
            };
            AddLowPriceSet(artAndCulture.Prices);
            AddArtAndCultureAtrributesSet(artAndCulture.Attributes);
            Repository.Category.Add(artAndCulture);

            //health and medicine
            var healthAndMedicine = new Category
            {
                Name = "Zdrowie i medycyna",
                Description = "Zdrowie, medycyna oraz choroby",
                LongDescription = "MISSING_LONG_DESC",
                Order = 6
            };
            AddPrestigePriceSet(healthAndMedicine.Prices);
            AddHealthAndMedicineAtrributesSet(healthAndMedicine.Attributes);
            Repository.Category.Add(healthAndMedicine);

            //automotive
            var automotive = new Category
            {
                Name = "Motoryzacja",
                Description = "Samochody, motocykle, ciężarówki oraz inne pojazdy. Naprawa, konserwacja, kupno oraz sprzedaż.",
                LongDescription = "MISSING_LONG_DESC",
                Order = 18
            };
            AddStandardPriceSet(automotive.Prices);
            AddAutomotiveAtrributesSet(automotive.Attributes);
            Repository.Category.Add(automotive);
        }

        private void GenerateUserOpinions()
        {
            var categories = Repository.Category.All();
            var category1 = categories.First();
            var category2 = categories.Last();

            var userOpinion = new Opinion
                                  {
                                      Name = "Zdziś",
                                      OpinionContent = "Takie to kolorowe jakieś srakieś.",
                                      AddressCity = "zapiździów wielki",
                                      OpinionMark = OpinionMark.Negative,
                                      IsGeneral = true,
                                      AuthorType = AuthorType.User
                                  };
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "Marian",
                                  OpinionContent = "Jo to bym dodoł kolorów tu.",
                                  AddressCity = "zapiździów mniejszy",
                                  OpinionMark = OpinionMark.Negative,
                                  IsGeneral = true,
                                  AuthorType = AuthorType.User
                              };
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "debil jakiś",
                                  OpinionContent = "Jakie to fajne.",
                                  AddressCity = "nju jork",
                                  OpinionMark = OpinionMark.Positive,
                                  IsGeneral = true,
                                  AuthorType = AuthorType.User
                              };
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "brzęczyszczykiewicz",
                                  OpinionContent = "rzeżuchą jedzie na sto metrów.",
                                  AddressCity = "powiat łękołody",
                                  OpinionMark = OpinionMark.Positive,
                                  IsGeneral = true,
                                  AuthorType = AuthorType.User
                              };
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "no-name-dude",
                                  OpinionContent = "blaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.",
                                  AddressCity = "wawa",
                                  OpinionMark = OpinionMark.Positive,
                                  IsGeneral = true,
                                  AuthorType = AuthorType.User
                              };
            userOpinion.Categories.Add(category1);
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "janusz",
                                  OpinionContent = "totalna kaszana, ściema jak się patrzy",
                                  AddressCity = "królówka",
                                  OpinionMark = OpinionMark.Negative,
                                  IsGeneral = false,
                                  AuthorType = AuthorType.User
                              };

            userOpinion.Categories.Add(category1);
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "zbyszek",
                                  OpinionContent = "poszło gładko, jak po maśle z wazeliną",
                                  AddressCity = "ciechomin",
                                  OpinionMark = OpinionMark.Positive,
                                  IsGeneral = false,
                                  AuthorType = AuthorType.User
                              };
            userOpinion.Categories.Add(category1);
            userOpinion.Categories.Add(category2);
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "Tom",
                                  OpinionContent = "rozstanie było ciężkie, pomogliście mi się podnieść",
                                  AddressCity = "LA",
                                  OpinionMark = OpinionMark.Positive,
                                  IsGeneral = false,
                                  AuthorType = AuthorType.User
                              };
            userOpinion.Categories.Add(category2);
            Repository.Opinion.Add(userOpinion);

            userOpinion = new Opinion
                              {
                                  Name = "Katie",
                                  OpinionContent = "wróć do mnie - tomek!",
                                  AddressCity = "LA",
                                  OpinionMark = OpinionMark.Negative,
                                  IsGeneral = false,
                                  AuthorType = AuthorType.User
                              };
            userOpinion.Categories.Add(category2);
            Repository.Opinion.Add(userOpinion);
        }
        
        private void GenerateExpertOpinions()
        {
            var categories = Repository.Category.All();
            var category1 = categories.First();
            var category2 = categories.Last();

            var expertOpinion = new Opinion
                                    {
                                        Name = "dr Lubicz",
                                        OpinionContent = "Zarabiam hajsu jak lodu i jeszcze więcej.",
                                        AddressCity = "Warszawa",
                                        OpinionMark = OpinionMark.Positive,
                                        IsGeneral = true,
                                        AuthorType = AuthorType.Expert
                                    };
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "dr Zosia",
                                    OpinionContent = "Kobieta jak ja, niezależna i obsceniczna może się tu realizować.",
                                    AddressCity = "Leśna Góra",
                                    OpinionMark = OpinionMark.Positive,
                                    IsGeneral = true,
                                    AuthorType = AuthorType.Expert
                                };
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "prof. Miodek",
                                    OpinionContent = "Zero poprawności językowej ci pytający mają, co za rzeźnia...",
                                    AddressCity = "Charków",
                                    OpinionMark = OpinionMark.Negative,
                                    IsGeneral = true,
                                    AuthorType = AuthorType.Expert
                                };
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "WWO",
                                    OpinionContent = "Każdy ponad każdym, innego traktuje jakby był niczym, wszyscy najmądrzejsi...",
                                    AddressCity = "Poznań",
                                    OpinionMark = OpinionMark.Negative,
                                    IsGeneral = true,
                                    AuthorType = AuthorType.Expert
                                };
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "Shakira",
                                    OpinionContent = "Waka waka e e e, caminamina he-e he",
                                    AddressCity = "Kongo",
                                    OpinionMark = OpinionMark.Positive,
                                    IsGeneral = true,
                                    AuthorType = AuthorType.Expert
                                };
            expertOpinion.Categories.Add(category1);
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "Franciszek Smuda",
                                    OpinionContent = "Nasza reprezentacja reprezentacja, przegrała choć była dobra dobra.",
                                    AddressCity = "Łódź",
                                    OpinionMark = OpinionMark.Negative,
                                    IsGeneral = false,
                                    AuthorType = AuthorType.Expert
                                };

            expertOpinion.Categories.Add(category1);
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "Pani Ania",
                                    OpinionContent = "Lubię krzyżówki, rebusy i szarady, system jest jak wszystkie trzy w jednym.",
                                    AddressCity = "Półtusk",
                                    OpinionMark = OpinionMark.Positive,
                                    IsGeneral = false,
                                    AuthorType = AuthorType.Expert
                                };
            expertOpinion.Categories.Add(category1);
            expertOpinion.Categories.Add(category2);
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "Krzysztof Ibisz",
                                    OpinionContent = "Dzięki systemowi jestem im starszy tym młodszy - kochani.",
                                    AddressCity = "Warszawa",
                                    OpinionMark = OpinionMark.Positive,
                                    IsGeneral = false,
                                    AuthorType = AuthorType.Expert
                                };
            expertOpinion.Categories.Add(category2);
            Repository.Opinion.Add(expertOpinion);

            expertOpinion = new Opinion
                                {
                                    Name = "Wojciech Jaruzelski",
                                    OpinionContent = "Towarzysze, gdybyśmy mieli taki system w stanie wojennym, nic by nie było problemem.",
                                    AddressCity = "Zakład Karny w Radzionkowie",
                                    OpinionMark = OpinionMark.Negative,
                                    IsGeneral = false,
                                    AuthorType = AuthorType.Expert
                                };
            expertOpinion.Categories.Add(category2);
            Repository.Opinion.Add(expertOpinion);
        }

        [DefaultRouting]
        public virtual ActionResult GeneratePartnerScript()
        {
            var model = Repository.Category.All();
            return PartialView(MVC.Development.Views.PartnerScript, model);
        }

        #region attributes

        private static void AddLawAndTaxesAttributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Dziedzina",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Księgowość"},
                        new CategoryAttributeOption {Value = "Prawo karne"},
                        new CategoryAttributeOption {Value = "Prawo podatkowe"},
                        new CategoryAttributeOption {Value = "Prawo patentowe"},
                        new CategoryAttributeOption {Value = "Prawo autorskie"}
                }
            });
        }
        private static void AddBusinessAndFinanceAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Dziedzina",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Inwestycje"},
                        new CategoryAttributeOption {Value = "Kredyty"},
                        new CategoryAttributeOption {Value = "Ubezpieczenia"},
                        new CategoryAttributeOption {Value = "Własna firma"},
                }
            });
        }
        private static void AddHealthAndMedicineAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Dziedzina",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Anoreksja i bulimia"},
                        new CategoryAttributeOption {Value = "Choroby i uzależnienia"},
                        new CategoryAttributeOption {Value = "Choroby mężczyzn"},
                        new CategoryAttributeOption {Value = "Choroby kobiece"},
                        new CategoryAttributeOption {Value = "Choroby psychiczne"},
                        new CategoryAttributeOption {Value = "Ciąża i poród"},
                        new CategoryAttributeOption {Value = "Dermatologia"},
                        new CategoryAttributeOption {Value = "Dieta i kondycja"},
                        new CategoryAttributeOption {Value = "Dojrzewanie"},
                        new CategoryAttributeOption {Value = "Intymność"},
                        new CategoryAttributeOption {Value = "Medycyna alternatywna"},
                        new CategoryAttributeOption {Value = "Ortopedia"},
                        new CategoryAttributeOption {Value = "Stomatologia"},
                        new CategoryAttributeOption {Value = "Wzrok i słuch"},
                        new CategoryAttributeOption {Value = "Ogólne"},
                    }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Importance = Importance.High,
                Name = "Wiek pacjenta",
                Options =
                    {
                        new CategoryAttributeOption {Value = "0-1"},
                        new CategoryAttributeOption {Value = "1-3"},
                        new CategoryAttributeOption {Value = "3-12"},
                        new CategoryAttributeOption {Value = "12-18"},
                        new CategoryAttributeOption {Value = "18-24"},
                        new CategoryAttributeOption {Value = "24-35"},
                        new CategoryAttributeOption {Value = "35-55"},
                        new CategoryAttributeOption {Value = "55-75"},
                        new CategoryAttributeOption {Value = "75 i wzwyż"},
                    }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Płeć pacjenta",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Kobieta"},
                        new CategoryAttributeOption {Value = "Mężczyzna"}
                    }
            });
        }
        private static void AddAutomotiveAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            var specialVehicles = new CategoryAttributeOption {Value = "Pojazdy specjalne"};

            var childSpecialVehicles = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Pojazdy specjalne",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Traktory"},
                        new CategoryAttributeOption {Value = "Kombajny"},
                        new CategoryAttributeOption {Value = "Dźwigi"}
                    },
                ParentOptions = {specialVehicles}
            };

            var vehicleChoices = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Rodzaj pojazdu",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Samochody"},
                        new CategoryAttributeOption {Value = "Motocykle"},
                        new CategoryAttributeOption {Value = "Autobusy"},
                        specialVehicles,
                    }
            };

            vehicleChoices.ChildAttributes.Add(childSpecialVehicles);
            attributes.Add(vehicleChoices);
        }
        private static void AddAnimalsAndVeterinaryAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            var smallMammals = new CategoryAttributeOption {Value = "Małe ssaki"};

            var childSmallMammals = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Małe ssaki",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Chomiki"},
                        new CategoryAttributeOption {Value = "Króliki"},
                        new CategoryAttributeOption {Value = "Świnki morskie"},
                        new CategoryAttributeOption {Value = "Inne"},
                    },
                ParentOptions = {smallMammals}
            };

            var animalChoices = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Gatunek zwierzęcia",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Psy"},
                        new CategoryAttributeOption {Value = "Koty"},
                        new CategoryAttributeOption {Value = "Ryby"},
                        smallMammals,
                        new CategoryAttributeOption {Value = "Gady"},
                        new CategoryAttributeOption {Value = "Płazy"},
                        new CategoryAttributeOption {Value = "Ptaki"},
                        new CategoryAttributeOption {Value = "Konie"},
                        new CategoryAttributeOption {Value = "Inne"},
                    }
            };

            animalChoices.ChildAttributes.Add(childSmallMammals);
            attributes.Add(animalChoices);
        }
        private static void AddTravelsAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Typ",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Turystyka"},
                        new CategoryAttributeOption {Value = "Emigracja"},
                }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Cel pytania",
                Importance = Importance.Medium,
                Options = {
                        new CategoryAttributeOption {Value = "Reklamacja"},
                        new CategoryAttributeOption {Value = "Pytanie organizacyjne"},
                        new CategoryAttributeOption {Value = "Inspiracje"},
                }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Region",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Afryka"},
                        new CategoryAttributeOption {Value = "Ameryka Północna"},
                        new CategoryAttributeOption {Value = "Ameryka Południowa"},
                        new CategoryAttributeOption {Value = "Austraila i Oceania"},
                        new CategoryAttributeOption {Value = "Azja"},
                        new CategoryAttributeOption {Value = "Europa Zachodnia"},
                        new CategoryAttributeOption {Value = "Europa Wschodnia"},
                    }             
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Okres",
                Importance = Importance.Medium,
                Options = {
                        new CategoryAttributeOption {Value = "Wiosna"},
                        new CategoryAttributeOption {Value = "Lato"},
                        new CategoryAttributeOption {Value = "Jesień"},
                        new CategoryAttributeOption {Value = "Zima"}
                    }
            });

            /*foreach (var country in CultureInfo.GetCultures(CultureTypes.SpecificCultures).OrderBy(c => c.DisplayName))
            {
                var countryTrim = new RegionInfo(country.Name);
                countries.Options.Add(new CategoryAttributeOption{ Value = countryTrim.DisplayName });
            }

            attributes.Add(countries);*/
        }
        private static void AddEntertainmentAndRecreationAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Rodzaj",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Gry komputerowe"},
                        new CategoryAttributeOption {Value = "Taniec"},
                        new CategoryAttributeOption {Value = "Wędkarstwo"},
                        new CategoryAttributeOption {Value = "Kolekcje"},
                        new CategoryAttributeOption {Value = "Broń i militaria"},
                        new CategoryAttributeOption {Value = "Muzyka"},
                        new CategoryAttributeOption {Value = "Inspiracja"},
                        new CategoryAttributeOption {Value = "Sport"},
                        new CategoryAttributeOption {Value = "Fotografia"},
                        new CategoryAttributeOption {Value = "Zabawy/konkursy"},
                }
            });
        }
        private static void AddCookeryAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Rodzaj",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Dietetyka"},
                        new CategoryAttributeOption {Value = "Przepisy"},
                        new CategoryAttributeOption {Value = "Wiedza"},
                }
            });
        }
        private static void AddChildrenAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Rodzaj",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Przygotowanie do narodzin"},
                        new CategoryAttributeOption {Value = "Przygotowanie do poczęcia"},
                        new CategoryAttributeOption {Value = "Wychowanie"},
                        new CategoryAttributeOption {Value = "Pielęgnacja/rozwój"},
                }
            });
        }
        private static void AddEventsAndCeremoniesAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Rodzaj wydarzenia",
                Importance = Importance.Highest,
                Options = {
                        new CategoryAttributeOption {Value = "Ślub/wesele"},
                        new CategoryAttributeOption {Value = "Pogrzeb"},
                        new CategoryAttributeOption {Value = "Urodziny"},
                        new CategoryAttributeOption {Value = "Imieniny"},
                        new CategoryAttributeOption {Value = "Randka"},
                        new CategoryAttributeOption {Value = "Prywatka"},
                        new CategoryAttributeOption {Value = "Andrzejki"},
                        new CategoryAttributeOption {Value = "Mikołajki"},
                        new CategoryAttributeOption {Value = "Walentynki"},
                        new CategoryAttributeOption {Value = "Wigilia"},
                        new CategoryAttributeOption {Value = "Wielkanoc"},
                        new CategoryAttributeOption {Value = "Inne"},
                        new CategoryAttributeOption {Value = "Bez okazji"},
                }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Kim jesteś",
                Importance = Importance.High,
                Options = {
                        new CategoryAttributeOption {Value = "Organizator"},
                        new CategoryAttributeOption {Value = "Gość"},
                }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Czego potrzebujesz?",
                Importance = Importance.Medium,
                Options = {
                        new CategoryAttributeOption {Value = "Pomysłu"},
                        new CategoryAttributeOption {Value = "Pomocy w organizacji"},
                        new CategoryAttributeOption {Value = "Inspiracji"},
                        new CategoryAttributeOption {Value = "Prezenty"},
                        new CategoryAttributeOption {Value = "Inne"},
                }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Okres",
                Importance = Importance.Medium,
                Options = {
                        new CategoryAttributeOption {Value = "Wiosna"},
                        new CategoryAttributeOption {Value = "Lato"},
                        new CategoryAttributeOption {Value = "Jesień"},
                        new CategoryAttributeOption {Value = "Zima"}
                    }
            });
        }
        private static void AddAboutMeAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            var personalDevelopment = new CategoryAttributeOption {Value = "Rozwój osobisty"};

            var childPersonalDevelopment = new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Rozwój osobisty",
                Importance = Importance.Highest,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Nauka języków"},
                        new CategoryAttributeOption {Value = "Coaching"},
                        new CategoryAttributeOption {Value = "Szkolenia"},
                    },
                ParentOptions = { personalDevelopment }
            }; 

            var topicChoices = new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Name = "Temat",
                Importance = Importance.Highest,
                Options = {
                        personalDevelopment,
                        new CategoryAttributeOption {Value = "Wygląd (Uroda i Styl)"},
                        new CategoryAttributeOption {Value = "Praca i kariera"},
                        new CategoryAttributeOption {Value = "Sny"},
                        new CategoryAttributeOption {Value = "Religia"},
                        new CategoryAttributeOption {Value = "Magia"},
                        new CategoryAttributeOption {Value = "Problemy"},
                        new CategoryAttributeOption {Value = "Wydarzenia paranormalne"},
                    }
            };

            topicChoices.ChildAttributes.Add(childPersonalDevelopment);
            attributes.Add(topicChoices);

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Importance = Importance.High,
                Name = "Wiek",
                Options =
                    {
                        new CategoryAttributeOption {Value = "0-12"},
                        new CategoryAttributeOption {Value = "12-18"},
                        new CategoryAttributeOption {Value = "18-24"},
                        new CategoryAttributeOption {Value = "24-35"},
                        new CategoryAttributeOption {Value = "35-55"},
                        new CategoryAttributeOption {Value = "55-75"},
                        new CategoryAttributeOption {Value = "75 i wzwyż"},
                    }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Płeć",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Kobieta"},
                        new CategoryAttributeOption {Value = "Mężczyzna"}
                    }
            });
        }
        private static void AddLoveAndRelationsAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.High,
                Name = "Temat",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Podryw/flirt"},
                        new CategoryAttributeOption {Value = "Orientacja seksualna"},
                        new CategoryAttributeOption {Value = "Relacja międzyludzka"},
                        new CategoryAttributeOption {Value = "Problemy miłosne"},
                    }
            });
        }
        private static void AddScienceAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            var foreignLanguage = new CategoryAttributeOption { Value = "Język obcy" };

            var childForeignLanguage = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Język obcy",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Angielski"},
                        new CategoryAttributeOption {Value = "Niemiecki"},
                        new CategoryAttributeOption {Value = "Rosyjski"},
                        new CategoryAttributeOption {Value = "Francuski"},
                        new CategoryAttributeOption {Value = "Hiszpański"},
                        new CategoryAttributeOption {Value = "Włoski"},
                        new CategoryAttributeOption {Value = "Chiński"},
                        new CategoryAttributeOption {Value = "Japoński"},
                        new CategoryAttributeOption {Value = "Portugalski"},
                    },
                ParentOptions = { foreignLanguage }
            };

            var childLevel = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Name = "Poziom",
                Importance = Importance.High,
                Options =
                    {
                        new CategoryAttributeOption {Value = "Początkujący"},
                        new CategoryAttributeOption {Value = "Średniozaawansowany"},
                        new CategoryAttributeOption {Value = "Zaawansowany"},
                        new CategoryAttributeOption {Value = "Profesjonalny"},
                    },
                ParentOptions = { foreignLanguage }
            };

            var typeChoices = new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Importance = Importance.High,
                Name = "Dziedzina",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Matematyka"},
                        new CategoryAttributeOption {Value = "Chemia"},
                        new CategoryAttributeOption {Value = "Informatyka"},
                        new CategoryAttributeOption {Value = "Język polski"},
                        foreignLanguage,
                        new CategoryAttributeOption {Value = "Biologia"},
                        new CategoryAttributeOption {Value = "Wychowanie fizyczne"},
                        new CategoryAttributeOption {Value = "Anatomia"},
                        new CategoryAttributeOption {Value = "Astronomia"},
                        new CategoryAttributeOption {Value = "Ekologia"},
                        new CategoryAttributeOption {Value = "Fizyka"},
                        new CategoryAttributeOption {Value = "Geografia"},
                        new CategoryAttributeOption {Value = "Geologia"},
                        new CategoryAttributeOption {Value = "Medycyna"},
                        new CategoryAttributeOption {Value = "Mechanika"},
                        new CategoryAttributeOption {Value = "Meteorologia"},
                        new CategoryAttributeOption {Value = "Przyroda"},
                        new CategoryAttributeOption {Value = "Technika"},
                        new CategoryAttributeOption {Value = "WOS"},
                        new CategoryAttributeOption {Value = "Filozofia"},
                        new CategoryAttributeOption {Value = "Historia"},
                    }
            };

            typeChoices.ChildAttributes.Add(childForeignLanguage);
            typeChoices.ChildAttributes.Add(childLevel);
            attributes.Add(typeChoices);

        }
        private static void AddInternetAndComputersAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.High,
                Name = "Rodzaj",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Naprawa komputera"},
                        new CategoryAttributeOption {Value = "Wybór oprogramowania"},
                        new CategoryAttributeOption {Value = "Instalacja"},
                        new CategoryAttributeOption {Value = "Gry"},
                        new CategoryAttributeOption {Value = "E-biznes"},
                    }
            });
        }
        private static void AddHouseAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.High,
                Name = "Temat",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Budowa"},
                        new CategoryAttributeOption {Value = "Remont"},
                        new CategoryAttributeOption {Value = "Sprzątanie/pielęgnacja"},
                        new CategoryAttributeOption {Value = "Projektowanie"},
                        new CategoryAttributeOption {Value = "Nieruchomości"},
                        new CategoryAttributeOption {Value = "Wystrój wnętrz"},
                        new CategoryAttributeOption {Value = "Inne"},
                    }
            });
        }
        private static void AddEquipmentRepairAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Importance = Importance.Highest,
                Name = "Temat",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Naprawa"},
                        new CategoryAttributeOption {Value = "Użytkowanie"},
                    }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.Highest,
                Name = "Typ urządzenia",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Urządzenia elektroniczne"},
                        new CategoryAttributeOption {Value = "Urządzenia domowe"},
                        new CategoryAttributeOption {Value = "Urządzenia optyczne"},
                        new CategoryAttributeOption {Value = "Instrumenty muzyczne"},
                        new CategoryAttributeOption {Value = "Ogólne"},
                        new CategoryAttributeOption {Value = "Inne"},
                    }
            });
        }
        private static void AddGardenAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.High,
                Name = "Rodzaj",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Kwiaty/Drzewa"},
                        new CategoryAttributeOption {Value = "Projektowanie"},
                        new CategoryAttributeOption {Value = "Pielęgnacja"},
                    }
            });
        }
        private static void AddArtAndCultureAtrributesSet(System.Collections.Generic.ICollection<CategoryAttribute> attributes)
        {
            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.MultiSelect,
                Importance = Importance.High,
                Name = "Temat",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Malarstwo"},
                        new CategoryAttributeOption {Value = "Rzeźba"},
                        new CategoryAttributeOption {Value = "Literatura"},
                        new CategoryAttributeOption {Value = "Proza"},
                        new CategoryAttributeOption {Value = "Poezja"},
                        new CategoryAttributeOption {Value = "Teatr"},
                    }
            });

            attributes.Add(new CategoryAttribute
            {
                Type = CategoryAttributeType.SingleSelect,
                Importance = Importance.High,
                Name = "Typ",
                Options =
                    {
                        new CategoryAttributeOption {Value = "Rzeczoznawstwo"},
                        new CategoryAttributeOption {Value = "Wycena"},
                    }
            });
        }
        
        #endregion

        private static void AddLowPriceSet(System.Collections.Generic.ICollection<Price> prices)
        {
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Low, Value = 5.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Medium, Value = 15.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.High, Value = 35.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Low, Value = 5.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Medium, Value = 15.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.High, Value = 35.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Low, Value = 5.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Medium, Value = 15.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.High, Value = 35.00m });
        }

        private static void AddStandardPriceSet(System.Collections.Generic.ICollection<Price> prices)
        {
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Low, Value = 10.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Medium, Value = 30.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.High, Value = 60.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Low, Value = 10.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Medium, Value = 30.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.High, Value = 60.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Low, Value = 10.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Medium, Value = 30.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.High, Value = 60.00m });
        }

        private static void AddPrestigePriceSet(System.Collections.Generic.ICollection<Price> prices)
        {
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Low, Value = 20.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.Medium, Value = 50.00m });
            prices.Add(new Price { Priority = ThreadPriority.Low, Verbosity = ThreadVerbosity.High, Value = 100.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Low, Value = 20.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.Medium, Value = 50.00m });
            prices.Add(new Price { Priority = ThreadPriority.Medium, Verbosity = ThreadVerbosity.High, Value = 100.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Low, Value = 20.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.Medium, Value = 50.00m });
            prices.Add(new Price { Priority = ThreadPriority.High, Verbosity = ThreadVerbosity.High, Value = 100.00m });
        }
    }

    public class OnlyDeveloperAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return httpContext.Request.Url.Port == 44300;
        }
    }
}
