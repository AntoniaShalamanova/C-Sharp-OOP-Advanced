using System;
using System.Linq;
using System.Text;

namespace _8PetClinics
{
    public class Clinic
    {
        public string Name { get; set; }

        private int rooms;

        private readonly Pet[] pets;

        public Clinic(string name, int rooms)
        {
            this.Name = name;
            this.Rooms = rooms;
            this.pets = new Pet[rooms];
        }

        public int Rooms
        {
            get
            {
                return rooms;
            }

            set
            {
                if (value % 2 == 0)
                {
                    throw new InvalidOperationException("Invalid Operation!");
                }

                rooms = value;
            }
        }

        public bool Add(Pet pet)
        {
            int centralRoom = this.Rooms / 2;

            int left = centralRoom;
            int right = centralRoom;

            while (left >= 0 && right < this.rooms)
            {
                if (this.pets[left] == null)
                {
                    this.pets[left] = pet;
                    return true;
                }
                else if (this.pets[right] == null)
                {
                    this.pets[right] = pet;
                    return true;
                }

                left--;
                right++;
            }

            return false;
        }

        public bool Release()
        {
            int centralRoom = this.Rooms / 2;

            int right = centralRoom;
            while (right < this.Rooms)
            {
                if (this.pets[right] != null)
                {
                    this.pets[right] = null;
                    return true;
                }

                right++;
            }

            int left = centralRoom;
            while (left >= 0)
            {
                if (this.pets[left] != null)
                {
                    this.pets[left] = null;
                    return true;
                }

                left--;
            }

            return false;
        }

        public bool HasEmptyRooms => this.pets.Any(x => x == null);

        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pet in this.pets)
            {
                string output = pet == null
                    ? "Room empty"
                    : pet.ToString();

                sb.AppendLine(output);
            }

            return sb.ToString().TrimEnd();
        }

        public string Print(int room)
        {
            int index = room - 1;

            return this.pets[index].ToString();
        }
    }
}
