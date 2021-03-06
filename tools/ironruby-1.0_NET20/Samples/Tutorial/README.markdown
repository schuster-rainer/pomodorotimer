IronRuby Tutorial
=================

Description
-----------

The application is an interactive tutorial, allowing users to use a REPL window to
follow along the teaching material

Topics covered
--------------

- Creating WPF UI using XAML
  - Using Blend for UI design
  - Creating WPF FlowDocument from RDoc SimpleMarkup text
- Creating domain-specific-languages (DSLs) in Ruby
- Creating an application that can be developed incrementally from an
  interactive session with ability to reload modified source files.
- Using a splash screen at application startup

Running the app
---------------

On the desktop:

    ir.exe wpf_tutorial.rb

On Silverlight:

    tutorial-sl.bat

Running the app interactively
-----------------------------

Launch ir.exe:

    load "wpf_tutorial.rb"
    #=> true
    # Edit wpf_tutorial.rb. For example, change the settings on the window in
    # the XAML
    reload # This should show the new window now...
    #=> true

Running the tests
-----------------

Both desktop and Silverlight:

    tests.bat

Just desktop:

    ir.exe test/test_console.rb

Just Silverlight:

    ruby %MERLIN_ROOT%Hosts\Silverlight\Scripts\run_tests.rb

