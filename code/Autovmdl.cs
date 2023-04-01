using Sandbox;
using Editor;
using System.IO;
using System.Collections.Generic;

namespace Autovmdl.Editor;

[Tool( "Autovmdl", "format_shapes", "Create models from a group of source models easily." )]
public class Autovmdl : DockWindow
{
	private ToolBar ToolBar { get; set; }

	public Autovmdl()
	{
		DeleteOnClose = true;
		Title = "Autovmdl";
		Size = new Vector2( 500f, 500f );
		SetWindowIcon( "format_shapes" );

		CreateUI();
		Show();
	}

	protected override void OnPaint()
	{
		base.OnPaint();
	}

	// MENU BAR

	private void BuildMenuBar()
	{
		var menu = MenuBar.AddMenu( "File" );
		menu.AddOption( "Open Folder", "folder_open", Close );
		menu.AddOption( "Quit", "disabled_by_default", Close );
	}

	// TOP TOOLBAR

	// VARIABLES
	private Option ConvertOption { get; set; }

	private void CreateToolBar()
	{
		ToolBar = new ToolBar(this, "AutovmdlToolbar");

		AddToolBar(ToolBar, ToolbarPosition.Left);

/*		var openFolder = ToolBar.AddOption( "Open Folder", "common/open.png" );
		openFolder.StatusText = "Open Folder";*/

		ConvertOption = ToolBar.AddOption( "Convert Models", "common/gotoexternal_sm.png" );
		ConvertOption.StatusText = "Convert";

		ToolBar.AddOption( "Cleanup", "common/revert_unchanged.png" );
	}

	// UI INITIALIZE: CREATE
	private void CreateUI()
	{
		CreateToolBar();
		BuildMenuBar();

		DockManager.RegisterDockType( "Console", "text_snippet", null, false );

		var console = TypeLibrary.Create( "ConsoleWidget", typeof( Widget ), new[] { this } ) as Widget;

		DockManager.AddDock( null, console );
	}

	[Event.Hotload]
	private void OnHotload()
	{
		RemoveToolBar( ToolBar );

		DockManager.Clear();
		MenuBar.Clear();

		CreateUI();
	}
}

