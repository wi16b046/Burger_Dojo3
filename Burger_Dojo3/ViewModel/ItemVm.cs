using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using System;

namespace Burger_Dojo3
{
    public class ItemVm : ViewModelBase
    {
        private ItemBase baseItem;

        public int Id
        {
            get { return baseItem.Id; }
            
        }

        public string Name
        {
            get { return baseItem.Name; }
            set { baseItem.Name = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return baseItem.Description; }
            set { baseItem.Description = value;
                RaisePropertyChanged();
            }
        }

        public string Room
        {
            get { return baseItem.Room; }
            set { baseItem.Room = value;
                RaisePropertyChanged();
            }
        }

        public int PosX
        {
            get { return baseItem.PosX; }
            set { baseItem.PosX = value;
                RaisePropertyChanged();
            }
        }

        public int PosY
        {
            get { return baseItem.PosY; }
            set
            {
                baseItem.PosY = value;
                RaisePropertyChanged();
            }
        }


        //Type the value is of
        public string ValueType
        {
            get {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValueType.Name;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValueType.Name;
                else
                    throw new Exception();
            }
            
        }

        //Type of Item (Sensor or Actuator)
        public Type ItemType
        {
            get
            {
                if (baseItem is ISensor)
                    return typeof(ISensor);
                else if (baseItem is IActuator)
                    return typeof(IActuator);
                else
                    throw new Exception();

            }
        }

        //Returns the name of the mode the sensor or actuator is in
        //Sets the Mode of an Item based on Enum in BaseModel
        public string Mode
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorMode.ToString();
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorMode.ToString();
                else
                    throw new Exception();
            }

            set
            {
                if (baseItem is ISensor)
                    (baseItem as BaseSensor).SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value, false);
                if (baseItem is IActuator)
                    (baseItem as BaseActuator).ActuatorMode = (ModeType)Enum.Parse(typeof(ModeType), value, false);

                RaisePropertyChanged();
            }
        }

        //Actual value of an item
        public object Value
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValue;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValue;
                else
                    throw new Exception();
            }

            set
            {
                if (baseItem is ISensor)
                    (baseItem as BaseSensor).SensorValue = value;
                else if (baseItem is IActuator)
                    (baseItem as BaseActuator).ActuatorValue = value;

                RaisePropertyChanged();
            }
        }

        public ItemVm(ISensor sensor)
        {
            baseItem = sensor as ItemBase;
        }

        public ItemVm(IActuator actuator)
        {
            baseItem = actuator as ItemBase;
        }


    }
}