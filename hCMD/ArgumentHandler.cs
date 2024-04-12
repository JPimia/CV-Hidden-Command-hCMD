namespace hCMD
{
    public static class ArgumentHandler
    {
        public static void OnArgumentsReceived(IArgumentDelegate handler, string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "/updatePath":
                        handler.OnSetupEnvironmentPath();
                        break;
                    case "/addProfile":
                        if (args.Length < 2)
                        {
                            handler.OnError("Trying to add a Profile without name and source.");
                            break;
                        }
                        else if (args.Length < 3)
                        {
                            handler.OnError("Trying to add a Profile without source file.");
                            break;
                        }

                        handler.OnAddProfile(args[1], args[2]);
                        break;
                    case "/profile":
                        if (args.Length < 2)
                        {
                            handler.OnError("Trying to load a Profile without name.");
                            break;
                        }

                        handler.OnLoadProfile(args[1]);
                        break;
                    default:
                        var arguments = string.Join(" ", args.Skip(1));

                        handler.OnRunCommand(args[0], arguments);
                        break;
                }
            }
            else
            {
                handler.OnShowUsageInstructions();
            }
        }
    }
}
