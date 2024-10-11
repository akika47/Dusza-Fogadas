using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Dusza.CustomControls
{
    public class GameRowItem : Control
    {
        public static readonly DependencyProperty GameNameProperty =
            DependencyProperty.Register("GameName", typeof(string), typeof(GameRowItem), new PropertyMetadata(null));

        public static readonly DependencyProperty OrganizerNameProperty =
            DependencyProperty.Register("OrganizerName", typeof(string), typeof(GameRowItem), new PropertyMetadata(null));

        public static readonly DependencyProperty ParticipantsProperty =
            DependencyProperty.Register("Participants", typeof(string), typeof(GameRowItem), new PropertyMetadata(null));

        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(string), typeof(GameRowItem), new PropertyMetadata(null));

        public GameRowItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameRowItem), new FrameworkPropertyMetadata(typeof(GameRowItem)));
        }

        public string GameNameProp
        {
            get => (string)GetValue(GameNameProperty);
            set => SetValue(GameNameProperty, value);
        }

        public string OrganizerNameProp
        {
            get => (string)GetValue(OrganizerNameProperty);
            set => SetValue(OrganizerNameProperty, value);
        }

        public string ParticipantsProp
        {
            get => (string)GetValue(ParticipantsProperty);
            set => SetValue(ParticipantsProperty, value);
        }

        public string EventsProp
        {
            get => (string)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }
    }
}
