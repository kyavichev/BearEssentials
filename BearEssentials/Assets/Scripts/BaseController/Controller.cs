using System;
using UnityEngine;

namespace Bears.Core
{
    /// <summary>
    /// An abstract controller class with services access and command and notification registrations/deregistration
    /// </summary>
    public abstract class Controller : MonoBehaviour
    {
        /// <summary>
        /// Cached messenger service
        /// </summary>
        protected IMessengerService Messenger { get; private set; }
        
        /// <summary>
        /// Cached model store service
        /// </summary>
        protected IModelStoreService ModelStore { get; private set; }
        
        /// <summary>
        /// The commands listener group
        /// </summary>
        protected ListenerGroup Commands { get; private set; }
        
        /// <summary>
        /// The notifications listener group
        /// </summary>
        protected ListenerGroup Notifications { get; private set; }

        protected virtual void OnValidate()
        {

        }

        protected void Awake()
        {
            Messenger = Services.Get<IMessengerService>();
            if (Messenger == null)
            {
                throw new Exception(
                    "Messenger service cannot be found.");
            }
            
            ModelStore = Services.Get<IModelStoreService>();
            if (ModelStore == null)
            {
                throw new Exception(
                    "Model store service cannot be found.");
            }

            Commands = new ListenerGroup(Messenger, MsgKind.Command, MsgKind.Request);
            Notifications = new ListenerGroup(Messenger, MsgKind.Notification);

            Initialize();
        }

        protected void OnDestroy()
        {
            Deinitialize();
        }

        protected virtual void OnEnable()
        {
            if (Messenger == null)
            {
                return;
            }
            
            RegisterCommands(Commands);
            RegisterNotifications(Notifications);
        }

        protected virtual void OnDisable()
        {
            if (Messenger == null || !Messenger.Available)
            {
                // return early if services have been destroyed
                return;
            }

            Commands?.RemoveAllListeners();
            Notifications?.RemoveAllListeners();
        }
        
        /// <summary>
        /// Override to initialize on awake
        /// </summary>
        protected virtual void Initialize()
        {
            
        }
        
        /// <summary>
        /// Override to de-initialize on destroy
        /// </summary>
        protected virtual void Deinitialize()
        {
            
        }
        
        /// <summary>
        /// Override to register command messages on awake. Message registered here will be de-registered on disable
        /// </summary>
        /// /// <param name="commands">A message registrar for commands</param>
        protected virtual void RegisterCommands(IMessageRegistrar commands)
        {
        }

        /// <summary>
        /// Override to register notification messages on enable. Messages registered here will be de-registered on disable
        /// </summary>
        /// <param name="notifications">A message registrar for notifications</param>
        protected virtual void RegisterNotifications(IMessageRegistrar notifications)
        {
        }
    }
}
