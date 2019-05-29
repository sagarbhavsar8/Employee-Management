using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.DomainModels;


namespace AddToMyCart.Repositories
{

    public interface IStatesRepository
    {
        void InsertState(State state);
        void UpdateStateDetails(State state);
        void DeleteState(int stateID);
        List<State> GetStates();
        List<State> GetStatesbyID(int stateID);
        int GetLatestStateID();
    }

    public class StatesRepository : IStatesRepository
    {
        AddToMyWebCartDbContext db;

        public StatesRepository()
        {
            db = new AddToMyWebCartDbContext();
        }

        public void DeleteState(int stateID)
        {
            State state = db.States.Where(s => s.StateID == stateID).SingleOrDefault();
            if(state != null)
            {
                db.States.Remove(state);
                db.SaveChanges();
            }
        }

        public int GetLatestStateID()
        {
            int stateID = db.States.Select(s => s.StateID).Max();
            return stateID;
        }

        public List<State> GetStates()
        {
            return db.States.ToList();
        }

        public List<State> GetStatesbyID(int stateID)
        {
            return db.States.Where(s => s.StateID == stateID).ToList();
        }

        public void InsertState(State state)
        {
            db.States.Add(state);
            db.SaveChanges();
        }

        public void UpdateStateDetails(State state)
        {
            State st = db.States.Where(s => s.StateID == state.StateID).SingleOrDefault();
            if (st != null)
            {
                st.StateName = state.StateName;
                db.SaveChanges();
            }
        }
    }
}
