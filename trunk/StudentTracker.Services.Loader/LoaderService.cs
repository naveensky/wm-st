using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository.MongoDb;
using StudentTracker.Services.Core;

namespace StudentTracker.Services.Loader {
    public class LoaderService {
        public void LoadData() {
            DropStudents();
            DropTeachers();
            LoadCourses();
            //LoadStudyCenters();
            //LoadTeacher();
            //LoadDummyUser();
        }

        public void CreateAdmin() {
            using (var repo = new MongoRepository<User>(CoreService.GetServer())) {
                repo.Drop();
                var admin = new User {
                    Username = "Admin",
                    Password = "L0@d123"
                };
                repo.Save(admin);
            }
        }

        public void DeleteAllAppointments() {
            using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                var students = repo.Collection;
                foreach (var student in students) {
                    if (student.Appointments != null) {
                        student.Appointments.Clear();
                        repo.Save(student);
                    }

                }
            }
        }

        public void AssignStudyCenter() {
            using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                using (var studyCenterRepo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                    var studyCenter = studyCenterRepo.Collection.First();
                    var students = repo.Collection;
                    foreach (var student in students) {
                        student.StudyCenter = studyCenter;
                        repo.Save(student);
                    }
                }
            }
        }

        public void LoadStudyCenters() {
            using (var repo = new MongoRepository<StudyCenter>(CoreService.GetServer())) {
                repo.Drop();
                var subhashNagar = new StudyCenter {
                    Id = ObjectId.NewObjectId(),
                    Name = "Subhash Nagar"
                };

                var rajouri = new StudyCenter {
                    Id = ObjectId.NewObjectId(),
                    Name = "Rajouri Garden"
                };

                var preetVihar = new StudyCenter {
                    Id = ObjectId.NewObjectId(),
                    Name = "Preet Vihar"
                };

                var hauzKhas = new StudyCenter {
                    Id = ObjectId.NewObjectId(),
                    Name = "Hauz Khas"
                };

                var noida = new StudyCenter {
                    Id = ObjectId.NewObjectId(),
                    Name = "Noida"
                };

                repo.Save(subhashNagar);
                repo.Save(rajouri);
                repo.Save(preetVihar);
                repo.Save(hauzKhas);
                repo.Save(noida);
            }
        }

        private void DropStudents() {
            using (var repo = new MongoRepository<Student>(CoreService.GetServer())) {
                repo.Drop();

            }
        }

        private void DropTeachers() {
            using (var repo = new MongoRepository<Teacher>(CoreService.GetServer())) {
                repo.Drop();
            }
        }

        private void LoadDummyUser() {
            var repo = new MongoRepository<User>(CoreService.GetServer());
            var user = new User {
                Name = "Admin",
                Username = "admin",
                Password = "asdf1234"
            };
            repo.Save(user);
        }

        private void LoadCourses() {
            var repo = new MongoRepository<Course>(CoreService.GetServer());
            repo.Drop();

            var awa = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "AWA"
            };

            var cr = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "CR"
            };

            var rc = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "RC"
            };

            var sc = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "SC"
            };

            var quant = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "quant"
            };

            var vocab = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "Vocab"
            };

            var writing = new Course {
                Id = ObjectId.NewObjectId(),
                Name = "Writing"
            };

            var gmat = new Course {
                Name = "GMAT",
                Children = new List<Course> { awa, rc, cr, sc, quant }
            };

            var gre = new Course {
                Name = "GRE",
                Children = new List<Course> { vocab, rc, awa, quant }
            };

            var sat = new Course {
                Name = "SAT",
                Children = new List<Course> { vocab, quant, writing }
            };

            repo.Save(gmat);
            repo.Save(gre);
            repo.Save(sat);
        }

        private void LoadTeacher() {
            var repo = new MongoRepository<Teacher>(CoreService.GetServer());

            repo.Drop();

            var gmatTeacher = new Teacher {
                Mobile = "9810904555",
                Name = "GMAT Teacher"
            };

            var greTeacher = new Teacher {
                Mobile = "9810904555",
                Name = "GRE Teacher"
            };

            var satTeacher = new Teacher {
                Mobile = "9810904555",
                Name = "SAT Teacher"
            };

            repo.Save(gmatTeacher);
            repo.Save(greTeacher);
            repo.Save(satTeacher);
        }

        public void LoadTimeSlot() {
            var repo = new MongoRepository<TimeSlot>(CoreService.GetServer());

            repo.Drop();

            var display1 = new TimeSlot {
                Display = "10:30AM - 12:00PM",
                Order = 1
            };

            var display2 = new TimeSlot {
                Display = "12:00PM - 1:30PM",
                Order = 1
            };

            var display3 = new TimeSlot {
                Display = "1:30PM - 3:00PM",
                Order = 1
            };

            var display4 = new TimeSlot {
                Display = "3:00PM - 4:30PM",
                Order = 1
            };

            var display5 = new TimeSlot {
                Display = "4:30PM - 6:00PM",
                Order = 1
            };

            var display6 = new TimeSlot {
                Display = "6:00PM - 7:30PM",
                Order = 1
            };

            repo.Save(display1);
            repo.Save(display2);
            repo.Save(display3);
            repo.Save(display4);
            repo.Save(display5);
            repo.Save(display6);
        }
    }
}