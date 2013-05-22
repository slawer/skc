using System;
using System.Xml;
using System.Threading;

namespace SKC
{
    /// <summary>
    /// Реализует команду блока отображения
    /// </summary>
    public class BlockViewCommand
    {
        /// <summary>
        /// имя узла в котором сохраняется команда
        /// </summary>
        protected const string blockName = "block";

        /// <summary>
        /// имя узла в котором сохраняется использовать для сбороса или нет
        /// </summary>
        protected const string useResetName = "reset";

        /// <summary>
        /// имя узла в котором сохраняется использовать для перехода или нет
        /// </summary>
        protected const string useNextStageName = "next";

        /// <summary>
        /// имя узла в котором сохраняется стату команды
        /// </summary>
        protected const string activedName = "actived";

        /// <summary>
        /// имя узла в котором сохраняется команда БО
        /// </summary>
        protected const string commandName = "command";

        protected bool useReset = false;                // использовать для сброса объема
        protected bool useNextStage = false;            // использовать для перехода на новый этап

        protected bool activ = false;                   // ипользовать команду для работы или нет

        protected ReaderWriterLockSlim slim;            // синхронизатор
        protected string command = string.Empty;        // команда блока отображения        

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public BlockViewCommand()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Определяет использовать команду для сброса объема или нет
        /// </summary>
        public bool UseForReset
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return useReset;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        useReset = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет использовать команду для перехода на новый этап работы или нет
        /// </summary>
        public bool UseForNextStage
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return useNextStage;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        useNextStage = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет команду сброса/перехода
        /// </summary>
        public string CommandDsn
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return command;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        command = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет активна команда или нет
        /// </summary>
        public bool Actived
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return activ;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        activ = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текстовое описание типа команды
        /// </summary>
        public string TextType
        {
            get
            {
                if (UseForReset && UseForNextStage)
                {
                    return "сброс/переход";
                }
                else
                {
                    if (UseForReset) return "сброс";
                    if (UseForNextStage) return "переход";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Сохранить команду БО в XML узел
        /// </summary>
        /// <param name="doc">Докумен в который осуществляется сохранение команды БО</param>
        /// <returns>Сохраненая команда</returns>
        public XmlNode SerializeToXml(XmlDocument doc)
        {
            try
            {
                XmlNode root = doc.CreateElement(blockName);

                XmlNode useResetNode = doc.CreateElement(useResetName);
                XmlNode useNextNode = doc.CreateElement(useNextStageName);

                XmlNode commandNode = doc.CreateElement(commandName);
                XmlNode activedNode = doc.CreateElement(activedName);

                useResetNode.InnerText = UseForReset.ToString();
                useNextNode.InnerText = UseForNextStage.ToString();

                commandNode.InnerText = CommandDsn;
                activedNode.InnerText = Actived.ToString();

                root.AppendChild(useResetNode);
                root.AppendChild(useNextNode);

                root.AppendChild(commandNode);
                root.AppendChild(activedNode);

                return root;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить команду БО из XML узела
        /// </summary>
        /// <param name="root">Узел в котором сохранена команда</param>
        public void DeserializeFromXml(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.Name == blockName && root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case useResetName:

                                    try
                                    {
                                        UseForReset = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case useNextStageName:

                                    try
                                    {
                                        UseForNextStage = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case commandName:

                                    try
                                    {
                                        CommandDsn = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case activedName:

                                    try
                                    {
                                        Actived = bool.Parse(child.InnerText);
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}